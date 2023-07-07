using DataImporter.Data;
using DataImporter.Transfer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.UnitOfWorks
{
    public interface ITransferUnitOfWork : IUnitOfWork
    {
        IGroupRepository Groups { get; }
        IImportRepository Imports { get; }
        IExportRepository Exports { get; }
        IExcelDataRepository  ExcelDatas { get; }
        IColumnDataRepository ColumnDatas { get; }
        IExcelFieldRepository ExcelFields { get; }
    }
}
