using DataImporter.Transfer.Entities;
using DataImporter.User.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Contexts
{
    public class TransferDbContext : DbContext , ITransferDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public TransferDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                 .ToTable("AspNetUsers", x => x.ExcludeFromMigrations())
                  .HasMany<Group>()
                  .WithOne(x => x.User);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<Export> Exports { get; set; }
        public DbSet<ColumnData> ColumnDatas { get; set; }
        public DbSet<ExcelData> ExcelDatas { get; set; }
        public DbSet<ExcelFieldData> ExcelFieldDatas { get; set; }

    }
}
