using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.Exports
{
    public class ExportModel
    {
        public int GroupId { get; set; }
        public List<ExcelFieldData> DataForExcel { get; set; }

        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IDateTimeUtility _dateTimeUtility;
        private IExportService  _exportService;

        public ExportModel()
        {

        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _exportService = _scope.Resolve<IExportService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public ExportModel(IMapper mapper, IDateTimeUtility dateTimeUtility, IExportService exportService)
        {
            _mapper = mapper;
            _exportService = exportService;
            _dateTimeUtility = dateTimeUtility;
        }


        public void GetTableData(int groupId)
        {
            GroupId = groupId;
            DataForExcel = _exportService.GetTableData(groupId);
        }
    }
}
