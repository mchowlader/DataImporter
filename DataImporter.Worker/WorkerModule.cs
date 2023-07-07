using Autofac;
using DataImporter.ExcelFileReader;
using DataImporter.Worker.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker
{
    public class WorkerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly IConfiguration _configuration;

        public WorkerModule(string connectionStringName, string migrationAssemblyName,
            IConfiguration configuration)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ImportModel>().AsSelf();
            builder.RegisterType<HelperExcelDataRead>().AsSelf();
            builder.RegisterType<DeleteModel>().AsSelf();

            base.Load(builder);
        }
    }
}
