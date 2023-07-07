using Autofac;
using AutoMapper;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class HomeModel
    {
        public int Groups { get; set; }
        public int Imports { get; set; }
        public int Exports { get; set; }


        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IGroupService _groupService;

        public HomeModel()
        {
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _groupService = _scope.Resolve<IGroupService>();
        }

        public void CountHomeProperty(Guid id)
        {
            var count = _groupService.CountHomeProperty(id);
            Groups = count.Groups;
        }


        public HomeModel(IMapper mapper, IGroupService groupService)
        {
            _mapper = mapper;
            _groupService = groupService;
        }
    }
}
