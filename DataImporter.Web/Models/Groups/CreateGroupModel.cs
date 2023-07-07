using AutoMapper;
using DataImporter.Transfer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DataImporter.Transfer.BusinessObjects;
using System.ComponentModel.DataAnnotations;
using DataImporter.Common.Utilities;
using DataImporter.User.Services;
using DataImporter.User.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Web.Models.Groups
{
    public class CreateGroupModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }

        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;
        private IGroupService  _groupService;
        private ILifetimeScope _scope;

        public CreateGroupModel()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _groupService = _scope.Resolve<IGroupService>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
        }

        public CreateGroupModel(IMapper mapper, IDateTimeUtility dateTimeUtility, 
            IGroupService groupService)
        {
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
            _groupService = groupService;
        }

        public void CreateGroup()
        {
            var group = new Group()
            {
                GroupName = GroupName,
                UserId = UserId,
                CreateDate = _dateTimeUtility.Now
            };
            _groupService.CreateGroup(group);
        }

    }
}
