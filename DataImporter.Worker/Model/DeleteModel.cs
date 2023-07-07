using Autofac;
using DataImporter.Transfer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker.Model
{
    public class DeleteModel
    {
        private ILifetimeScope _scope;
        private IImportService _importService;

        public DeleteModel()
        {

        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public DeleteModel(IImportService importService)
        {
            _importService = importService;
        }


        public void DeleteFile()
        {
            _importService.DeleteDoneItem();
        }

    }
}
