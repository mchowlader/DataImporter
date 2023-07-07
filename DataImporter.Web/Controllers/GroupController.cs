using DataImporter.Common.Utilities;
using DataImporter.User.Contexts;
using DataImporter.User.Entities;
using DataImporter.User.Services;
using DataImporter.Web.Models;
using DataImporter.Web.Models.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Autofac;
using Microsoft.Extensions.Logging;

namespace DataImporter.Web.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private ApplicationDbContext _dbContext;
        private readonly ILifetimeScope _scope;
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<GroupController> _logger;

        public GroupController(ILifetimeScope scope, ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager, ILogger<GroupController> logger)
        {
            _logger = logger;
            _scope = scope;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        
        public IActionResult Create()
        {
            var model = _scope.Resolve<CreateGroupModel>();
            return View(model);
        }
        //ok
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                try
                {
                    model.UserId = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                    model.CreateGroup();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Group Create Failed");
                    _logger.LogError(ex, "Group Create Failed");
                }
                
            }

            return View(model);

        }
        public IActionResult Groups()
        {
            return View();
        }
        //ok
        public JsonResult GetGroupDataByUser()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<ListGroupModel>();
            //model.UserId = _userManager.GetUserId(HttpContext.User);
            var id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            var data = model.GetGroupsByUser(dataTableModel,id);
            return Json(data);
        }
        //ok
        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<EditGroupModel>();
            model.EditGroup(id);
            return View(model);
        }
        //ok
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        model.Resolve(_scope);
                        model.Update();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Failed to update group");
                        _logger.LogError(ex, "Update Course Failed");
                    }
                }
            }

            return View(model);
        }

        //ok
        public IActionResult DeleteGroup(int id)
        {
            var model = _scope.Resolve<ListGroupModel>();
            model.GroupDelete(id);
            return RedirectToAction(nameof(Groups));
        }

        public IActionResult Contacts(int id)
        {
            var model = _scope.Resolve<ContactsModel>();
            model.Id = id;
            var userId = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            model.LoadGroupProperty(userId);
            var groupData = model.groupsList;
            groupData.Insert(0, new Transfer.BusinessObjects.Group { Id = 0, GroupName = "Select Group" });
            ViewBag.data = groupData;

            return View(model);
        }

        public JsonResult GetAllData(int id)
        {
          
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<ContactsModel>();
            var userId = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            var data = model.GetAllData(dataTableModel, userId, id);
            //var data2 = model.GetAllDataTest2( userId, id);

            return Json(data);

        }


        //test
       
        public IActionResult ViewGroupData()
        {

            return RedirectToAction(nameof(Contacts));
        }

    }
}
