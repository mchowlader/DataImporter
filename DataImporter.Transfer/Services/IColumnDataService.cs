using DataImporter.Transfer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public interface IColumnDataService
    {
        List<string> FileMatching(int groupId);
        void InsertColumnHeader(ColumnData column);
        List<ColumnData> GetColumnDataById(int groupId);
    }
}
