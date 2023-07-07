using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.ExcelFileCreate;
using DataImporter.Transfer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.Exports
{
    public class DownloadModel
    {
        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IDateTimeUtility _dateTimeUtility;
        private IExportService _exportService;
        private IGroupService _groupService ;
        private ICreateExcelSheet _createExcelSheet ;

        public DownloadModel()
        {

        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _exportService = _scope.Resolve<IExportService>();
            _mapper = _scope.Resolve<IMapper>();
            _groupService = _scope.Resolve<IGroupService>();
            _createExcelSheet = _scope.Resolve<ICreateExcelSheet>();
        }

        public DownloadModel(IMapper mapper, IDateTimeUtility dateTimeUtility, IExportService exportService,
            IGroupService groupService, ICreateExcelSheet createExcelSheet)
        {
            _mapper = mapper;
            _exportService = exportService;
            _groupService = groupService;
            _dateTimeUtility = dateTimeUtility;
            _createExcelSheet = createExcelSheet;
        }

        public async Task<(string path, string groupName)>Download(int groupId)
        {
            var groupDatas = _exportService.GetTableData(groupId);
            var groupInfo = _groupService.GetGroup(groupId);

            var path = await _createExcelSheet.CreateExcel(groupInfo.GroupName);
            return (path, groupInfo.GroupName);
            //return default;
        }

        public void GetTableData(int groupId)
        {
           var data = _exportService.GetTableData(groupId);


        }
    }
}
