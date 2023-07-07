using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DataImporter.ExcelFileCreate
{
    public class CreateExcelSheet : ICreateExcelSheet
    {

        public async Task<string> CreateExcel(/*List<ExcelFieldData> list , */string groupName)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Export",
                    $"{DateTime.Now.Ticks.ToString()}.xlsx");

            var file = new FileInfo(path);

            ExistsFileDelete(file);

            using var package = new ExcelPackage(file);
          
            var worksheet = package.Workbook.Worksheets.Add(groupName);
            //var i = 1;
            //foreach(var data in list)
            //{
            //    var j = 1;
            //    foreach(var value in data.ColumnData)
            //    {
            //        worksheet.Cells[i, j].Value = value;
            //        j++;
            //    }
            //    i++;

            //    await package.SaveAsync();
            //}
            await package.SaveAsync();
            package.Dispose();
            return path;
            
        }

        private static void ExistsFileDelete(FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }

    }
}
