using AutoMapper;
using BO = DataImporter.Transfer.BusinessObjects;
using EO = DataImporter.Transfer.Entities;
using DataImporter.Web.Models;
using DataImporter.Web.Models.Groups;
using DataImporter.Web.Models.Imports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<BO.Group, CreateGroupModel>().ReverseMap();
            CreateMap<BO.Group, EditGroupModel>().ReverseMap();
            CreateMap<BO.Group, ListGroupModel>().ReverseMap();
            CreateMap<BO.Import, ListImportModel>().ReverseMap();
        }
    }
}
