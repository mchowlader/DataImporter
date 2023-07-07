using Autofac;
using DataImporter.Web.Models.Exports;
using DataImporter.Web.Models.Imports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Controllers
{
    [Authorize]
    public class ExportController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<ExportController> _logger;

        public ExportController(ILifetimeScope scope, ILogger<ExportController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Exports()
        {
            return View();
        }

        public async Task <IActionResult> ExportToExcel(UploadModel model)
        {
            var exportModel = _scope.Resolve<DownloadModel>();

            if(ModelState.IsValid)
            {
                model.Resolve(_scope);
                await exportModel.Download(model.Id);
                exportModel.GetTableData(model.Id);//not full complete
            }
            return RedirectToAction(nameof(Exports));
        }



        public IActionResult Download(UploadModel model)
        {

            var exportModel = _scope.Resolve<DownloadModel>();
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
            }

            return View();
        }
    }
}
