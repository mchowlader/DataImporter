using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.ExcelFileCreate
{
    public interface ICreateExcelSheet
    {
         Task<string> CreateExcel(/*List<ExcelFieldData> list,*/ string groupName);
    }
}
