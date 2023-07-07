using DataImporter.Transfer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public interface IExcelDataService
    {
        void ExcelDataRow(ExcelData excelData);
        List<ExcelData> GetExcelDataId();
        List<ExcelData> GetExcelDataById(int groupId);
    }
}