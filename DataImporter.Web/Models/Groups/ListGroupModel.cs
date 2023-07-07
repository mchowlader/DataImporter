using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Transfer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.Groups
{
    public class ListGroupModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }

        private  IMapper _mapper;
        private  ILifetimeScope _scope;
        private  IGroupService _groupService;

        public ListGroupModel()
        {
            
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _groupService = _scope.Resolve<IGroupService>();
        }
        public ListGroupModel(IMapper mapper, IGroupService groupService)
        {
            _mapper = mapper;
            _groupService = groupService;
        }

        internal object GetGroupsByUser(DataTablesAjaxRequestModel dataTableModel, Guid id)
        {
            var data = _groupService.GetGroupsByUserId(
               dataTableModel.PageIndex,
               dataTableModel.PageSize,
               dataTableModel.SearchText,
               dataTableModel.GetSortText(new string[] { "GroupName", "CreateDate"}),id);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.GroupName,
                                record.CreateDate.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        public void GroupDelete(int id)
        {
            _groupService.GroupDelete(id);
        }

       
    }
}
