using Autofac;
using System;

namespace DataImporter.ExcelFileCreate

{
    public class CreateExcelModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateExcelSheet>().As<ICreateExcelSheet>()
             .InstancePerLifetimeScope();

            base.Load(builder);
        }

    }
}
