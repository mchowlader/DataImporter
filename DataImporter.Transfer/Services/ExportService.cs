using AutoMapper;
using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.UnitOfWorks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public class ExportService : IExportService
    {
        private readonly IMapper _mapper;
        private readonly ITransferUnitOfWork _transferUnitOfWork;

        public ExportService(IMapper mapper, ITransferUnitOfWork transferUnitOfWork)
        {
            _mapper = mapper;
            _transferUnitOfWork = transferUnitOfWork;
        }
         
        //need to correction this method
        public List<ExcelFieldData> GetTableData(int groupId)
        {
            var excelFieldList = new List<ExcelFieldData>();
            var excelDataList = new List<ExcelFieldData>();

            var excelData = _transferUnitOfWork.ExcelDatas.Get(m => m.GroupId == groupId, "Group");



            foreach (var x in excelData)
            {
                var excelDataObj = new ExcelData
                {
                    Id = x.Id,
                    GroupId = x.GroupId
                };

                //excelDataList.Add(excelDataObj);


                var excelField = _transferUnitOfWork.ExcelFields.Get(n => n.ExcelDataId == excelDataObj.Id, "ExcelData");


                foreach (var excel in excelField)
                {


                    if (x.Id == excel.ExcelDataId)
                    {
                        var ee = _mapper.Map<ExcelFieldData>(excel);
                        //excelFieldList.Add(new ExcelFieldData {ColumnData = excel });

                    }
                }

                //var resultData2 = (from ED in excelData
                //                   join EFD in excelField on ED.Id equals EFD.ExcelDataId
                //                   where ED.Id == EFD.ExcelDataId
                //                   select _mapper.Map<ExcelFieldData>(EFD)).ToList();


                //excelFieldList.Add(resultData2.ToString());
            }

            return excelFieldList;
        }

    }
}
