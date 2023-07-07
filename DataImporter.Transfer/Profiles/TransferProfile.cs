using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = DataImporter.Transfer.Entities;
using BO = DataImporter.Transfer.BusinessObjects;

namespace DataImporter.Transfer.Profiles
{
    public class TransferProfile : Profile
    {
        public TransferProfile()
        {
            CreateMap<BO.Group, EO.Group>().ReverseMap();
            CreateMap<BO.Import, EO.Import>().ReverseMap();
            CreateMap<BO.ColumnData, EO.ColumnData>().ReverseMap();
            CreateMap<BO.ExcelData, EO.ExcelData>().ReverseMap();
            CreateMap<BO.ExcelFieldData, EO.ExcelFieldData>().ReverseMap();
        }
    }
}
