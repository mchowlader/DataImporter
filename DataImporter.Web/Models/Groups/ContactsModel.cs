using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ContactsModel
    {
        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IDateTimeUtility _dateTimeUtility;
        private IGroupService _groupService;

        public ContactsModel()
        {
           
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _groupService = _scope.Resolve<IGroupService>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
        }
        public ContactsModel(IMapper mapper, IDateTimeUtility dateTimeUtility,
            IGroupService groupService)
        {
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
            _groupService = groupService;
        }
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Group { get; set; }
        public string DateTo { get; set; }
        public string DateFrom { get; set; }

        public List<DataStore> TestingListData { get; set; }
        //testing
        public IList<ExcelFieldData> Datalist { get; set; }

        public IList<Group> groupsList { get; set; }

        public void LoadGroupProperty(Guid userId)
        {

            groupsList = _groupService.LoadGroupProperty(userId);
        }

        //need
        public object GetAllData(DataTablesAjaxRequestModel dataTableModel, Guid userId, int id)
        {
            var data = _groupService.GetAllData(
              dataTableModel.PageIndex,
              dataTableModel.PageSize,
              dataTableModel.SearchText,
              dataTableModel.GetSortText(new string[] { "Name", "Value" }), id, userId);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Value.ToString(),
                        }
                    ).ToArray()
            };
        }

    }

}
