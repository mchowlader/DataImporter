using Autofac;
using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker.Model
{
    public class ImportModel
    {
        private IImportService _importService;
        private IExcelDataService _excelDataService;
        private IColumnDataService _columnDataService;
        private IExcelFieldService _excelFieldService;
        private ILifetimeScope _scope;
        private HelperExcelDataRead _helperExcelDataRead;
        public ImportModel()
        {
            
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _columnDataService = _scope.Resolve<IColumnDataService>();
            _excelFieldService = _scope.Resolve<IExcelFieldService>();
            _excelDataService = _scope.Resolve<IExcelDataService>();
            _importService = _scope.Resolve<IImportService>();
            _helperExcelDataRead = _scope.Resolve<HelperExcelDataRead>();
        }

        public ImportModel(IImportService importService, IColumnDataService columnDataService,
            HelperExcelDataRead helperExcelDataRead, IExcelDataService excelDataService, 
            IExcelFieldService excelFieldService)
        {

            _importService = importService;
            _excelDataService = excelDataService;
            _columnDataService = columnDataService;
            _helperExcelDataRead = helperExcelDataRead;
            _excelFieldService = excelFieldService;

        }

        public int GroupId { get; set; }
        public string Path { get; set; }
        public List<Import> PendingItemList { get; set; }

        public void GetPendingItem()
        {
             PendingItemList = _importService.GetPendingItem();
        }

        public void ExcelDataInser()
        {
            foreach (var item in PendingItemList)
            {

                if (item.Status == "Pending")
                {
                    _importService.StatusUpdate(item.GroupId);

                    Path = item.FilePath;
                    GroupId = item.GroupId;
                    var allExcelData = 0;
                    var AllExcelRowData = _helperExcelDataRead.ReadExcelData(allExcelData, Path);

                    for (var m = 1; m < AllExcelRowData.Count; m++)
                    {
                        var excelData = new ExcelData()
                        {
                            GroupId = item.GroupId

                        };

                        _excelDataService.ExcelDataRow(excelData);

                        var excelDataList = _excelDataService.GetExcelDataById(GroupId);

                        var excelDataLastItem = excelDataList.LastOrDefault();
                        var ColumnDataList = _columnDataService.GetColumnDataById(GroupId);

                        for (var n = 0; n < ColumnDataList.Count; n++)
                        {
                            var excelFieldDat = new ExcelFieldData()
                            {
                                Name = ColumnDataList[n].ColumnName,
                                Value = AllExcelRowData[m].ColumnData[n],
                                ExcelDataId = excelDataLastItem.Id
                            };

                            _excelFieldService.InsertExcedFieldData(excelFieldDat);

                        }

                    }

                    _importService.UpdateProcessStatus(item.GroupId);

                }
            }
        }

    }
}
