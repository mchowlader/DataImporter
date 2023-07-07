using Autofac;
using DataImporter.Web.Models;
using DataImporter.Web.Models.Exports;
using DataImporter.Web.Models.Groups;
using DataImporter.Web.Models.Imports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateGroupModel>().AsSelf();
            builder.RegisterType<ListGroupModel>().AsSelf();
            builder.RegisterType<EditGroupModel>().AsSelf();
            builder.RegisterType<ContactsModel>().AsSelf();

            builder.RegisterType<HomeModel>().AsSelf();

            builder.RegisterType<UploadModel>().AsSelf();
            builder.RegisterType<ListImportModel>().AsSelf();
            builder.RegisterType<CreateImportModel>().AsSelf();
            builder.RegisterType<ExportModel>().AsSelf();
            builder.RegisterType<DownloadModel>().AsSelf();

            base.Load(builder);
        }
    }
}
