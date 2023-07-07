using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Services;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.Imports
{
    public class CreateImportModel
    {
        public int Id { get; set; }
        public string DateTo { get; set; }
        public string DateFrom { get; set; }

        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IDateTimeUtility _dateTimeUtility;
        private IImportService _importService;
        private IColumnDataService _columnDataService;

        public CreateImportModel()
        {

        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _importService = _scope.Resolve<IImportService>();
            _columnDataService = _scope.Resolve<IColumnDataService>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
        }
        public CreateImportModel(IMapper mapper, IDateTimeUtility dateTimeUtility,
            IImportService importService, IColumnDataService  columnDataService)
        {
            _mapper = mapper;
            _columnDataService = columnDataService;
            _dateTimeUtility = dateTimeUtility;
            _importService = importService;
        }

        HelperExcelDataRead helperExcelDataRead = new HelperExcelDataRead();

        public void CreateImportHistory(int id, string filePath, string excelFileName)
        {

            var importsData = new Import
            {
                GroupId = id,
                ImportDate = _dateTimeUtility.Now,
                ExcelFileName = excelFileName,
                FilePath = filePath,
                Status = "Pending"
            };
            _importService.UploadExcelFile(importsData);
        }

        public bool FileMatching(int groupId, string filePath)
        {
            List<string> columnList = new List<string>();

            columnList = _columnDataService.FileMatching(groupId);

            if (columnList != null)
            {

            var columnDatas = helperExcelDataRead.ReadExcelData(1, filePath);

                for (var m = 0; m < columnList.Count; m++)
                {
                    if (columnDatas[0].ColumnData.Count == columnList.Count && columnDatas[0].ColumnData[m] == columnList[m])
                    {
                        continue;
                    }
                    else
                        throw new InvalidProgramException("Excel Column Dose not Match");
                }

                return true;
            }

            else
                return false;

        }

        public void InsertTableHeader(int id, string filePath)
        {

            var columnDatas = helperExcelDataRead.ReadExcelData(1, filePath);


            for (var m = 0; m < columnDatas[0].ColumnData.Count; m++)
            {
                var column = new ColumnData()
                {
                    GroupId = id,
                    ColumnName = columnDatas[0].ColumnData[m],
                    ColumnNumber = m
                };

                _columnDataService.InsertColumnHeader(column);

            }

        }
    }
}
