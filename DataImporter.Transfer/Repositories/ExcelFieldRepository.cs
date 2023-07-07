using DataImporter.Data;
using DataImporter.Transfer.Contexts;
using DataImporter.Transfer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Repositories
{
    public class ExcelFieldRepository : Repository<ExcelFieldData, int>, IExcelFieldRepository
    {
        public ExcelFieldRepository(ITransferDbContext transferDbContext)
          : base((DbContext)transferDbContext)
        {

        }
    }
}
