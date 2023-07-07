using DataImporter.Data;
using DataImporter.Transfer.Contexts;
using DataImporter.Transfer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.UnitOfWorks
{
    public class TransferUnitOfWork : UnitOfWork, ITransferUnitOfWork
    {
        public IGroupRepository Groups { get; private set; }
        public IImportRepository Imports { get; private set; }
        public IExportRepository Exports { get; private set; }
        public IExcelDataRepository ExcelDatas { get; private set; }
        public IColumnDataRepository ColumnDatas { get; private set; }
        public IExcelFieldRepository ExcelFields { get; private set; }

        public TransferUnitOfWork(ITransferDbContext transferDbContext, 
            IGroupRepository groupRepository, IImportRepository importRepository, 
            IExportRepository exportRepository, IExcelDataRepository excelDataRepository,
            IColumnDataRepository columnDataRepository, IExcelFieldRepository excelFieldRepository)
           : base((DbContext)transferDbContext)
        {
            Groups = groupRepository;
            Imports = importRepository;
            Exports = exportRepository;
            ExcelDatas = excelDataRepository;
            ColumnDatas = columnDataRepository;
            ExcelFields = excelFieldRepository;
        }

    }
}
