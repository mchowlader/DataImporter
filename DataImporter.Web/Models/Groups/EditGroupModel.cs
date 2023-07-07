using Autofac;
using AutoMapper;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.Groups
{
    public class EditGroupModel 
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string GroupName { get; set; }

        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime CreateDate { get; set; }

        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IGroupService _groupService;

        public EditGroupModel()
        {
            
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _groupService = _scope.Resolve<IGroupService>();
        }
        public EditGroupModel(IMapper mapper, IGroupService groupService)
        {
            _mapper = mapper;
            _groupService = groupService;
        }

        public void EditGroup(int id)
        {
            var group = _groupService.GetGroup(id);

            _mapper.Map(group,this);
        }

        public void Update()
        {
            var group = _mapper.Map<Group>(this);
            _groupService.UpdateGroup(group);
        }
    }
}
