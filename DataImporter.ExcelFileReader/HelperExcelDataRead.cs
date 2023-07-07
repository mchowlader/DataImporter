using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DataImporter.ExcelFileReader
{
    public class HelperExcelDataRead
    {
        public List<DataStore> ReadExcelData(int nedColmun , string filePath)
        {
            if(nedColmun <= 0)
            {
                nedColmun = int.MaxValue;
            }

            var RowData = new List<DataStore>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = excelReader.AsDataSet();

                    DataTable dataTable = result.Tables[0];



                    for (var i = 0; i < dataTable.Rows.Count && i < nedColmun; i++)
                    {
                        var array = new string[dataTable.Columns.Count];

                        for (var j = 0; j < array.Length; j++)
                        {
                            array[j] = dataTable.Rows[i][j].ToString();
                        }

                        RowData.Add(new DataStore { ColumnData = array });
                    }

                }
            }


            return RowData;
        }
    }
}
