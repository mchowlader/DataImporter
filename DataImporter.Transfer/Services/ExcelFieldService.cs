using AutoMapper;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public class ExcelFieldService : IExcelFieldService
    {
        private readonly IMapper _mapper;

        private readonly ITransferUnitOfWork _transferUnitOfWork;


        public ExcelFieldService(IMapper mapper, ITransferUnitOfWork transferUnitOfWork)
        {
            _mapper = mapper;
            _transferUnitOfWork = transferUnitOfWork;
        }

        public void InsertExcedFieldData(ExcelFieldData excelFieldDat)
        {
            _transferUnitOfWork.ExcelFields.Add(
                new Entities.ExcelFieldData()
                {
                    Name = excelFieldDat.Name,
                    Value = excelFieldDat.Value,
                    ExcelDataId = excelFieldDat.ExcelDataId
                });
            _transferUnitOfWork.Save();
        }
    }
}
