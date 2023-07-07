using Autofac;
using DataImporter.User.Entities;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private readonly ILifetimeScope _scope;

        public HomeController(ILifetimeScope scope, ILogger<HomeController> logger, 
            UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            var model = _scope.Resolve<HomeModel>();
            model.CountHomeProperty(id);
            return View(model);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
