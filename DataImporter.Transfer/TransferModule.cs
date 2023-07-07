using Autofac;
using DataImporter.Transfer.Contexts;
using DataImporter.Transfer.Repositories;
using DataImporter.Transfer.Services;
using DataImporter.Transfer.UnitOfWorks;
using System;

namespace DataImporter.Transfer
{
    public class TransferModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public TransferModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransferDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<TransferDbContext>().As<ITransferDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();


            builder.RegisterType<TransferUnitOfWork>().As<ITransferUnitOfWork>()
               .InstancePerLifetimeScope();

            builder.RegisterType<GroupService>().As<IGroupService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<GroupRepository>().As<IGroupRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ImportService>().As<IImportService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImportRepository>().As<IImportRepository>()
                .InstancePerLifetimeScope(); 

            builder.RegisterType<ExportService>().As<IExportService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExportRepository>().As<IExportRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ColumnDataService>().As<IColumnDataService>()
               .InstancePerLifetimeScope();
            builder.RegisterType<ColumnDataRepository>().As<IColumnDataRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ExcelDataRepository>().As<IExcelDataRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExcelDataService>().As<IExcelDataService>()
             .InstancePerLifetimeScope();
            
            builder.RegisterType<ExcelFieldRepository>().As<IExcelFieldRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExcelFieldService>().As<IExcelFieldService>()
             .InstancePerLifetimeScope();


            base.Load(builder);
        }

    }
}
