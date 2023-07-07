using DataImporter.Worker.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ImportModel _importModel;
        private readonly DeleteModel _deleteModel;

        public Worker(ILogger<Worker> logger, ImportModel importModel, DeleteModel deleteModel)
        {
            _logger = logger;
            _deleteModel = deleteModel;
            _importModel = importModel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _importModel.GetPendingItem();
                _importModel.ExcelDataInser();
                _deleteModel.DeleteFile();

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
