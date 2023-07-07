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
    public class ExcelDataService : IExcelDataService
    {
        private readonly IMapper _mapper;
        
        private readonly ITransferUnitOfWork _transferUnitOfWork;


        public ExcelDataService(IMapper mapper, ITransferUnitOfWork transferUnitOfWork)
        {
            _mapper = mapper;
            _transferUnitOfWork = transferUnitOfWork;
        }

        public void ExcelDataRow(ExcelData excelData)
        {
            _transferUnitOfWork.ExcelDatas.Add(
                new Entities.ExcelData()
                { 
                    GroupId = excelData.GroupId
                });

            _transferUnitOfWork.Save();
        }

        public List<ExcelData> GetExcelDataId()
        {
            List<ExcelData> ExcelDataList = new List<ExcelData>();

            var excelDataEntity = _transferUnitOfWork.ExcelDatas.GetAll();

            foreach (var excel in excelDataEntity)
            {
                var excelsRowId = new ExcelData()
                {
                    Id = excel.Id
                };
                ExcelDataList.Add(excelsRowId);
            }
            return ExcelDataList;
        }


        public List<ExcelData> GetExcelDataById(int groupId)
        {
            List<ExcelData> excelDataList = new List<ExcelData>();
            var excelDataEntity = _transferUnitOfWork.ExcelDatas.Get(m => m.GroupId == groupId, "Group");

            foreach (var excel in excelDataEntity)
            {
                var excelObj = new ExcelData()
                {
                    GroupId = excel.GroupId,
                    Id = excel.Id
                };

                excelDataList.Add(excelObj);
            }

            return excelDataList;
        }
    }
}
