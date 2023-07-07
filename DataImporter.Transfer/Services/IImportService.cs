using DataImporter.Transfer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public interface IImportService
    {
        (IList<Import> records, int total, int totalDisplay) GetImportsData(int pageIndex,
            int pageSize, string searchText, string sortText, Guid id);
        void UploadExcelFile(Import importsData);
        List<Import> GetPendingItem();
        void StatusUpdate(int groupId);
        void UpdateProcessStatus(int groupId);
        void DeleteDoneItem();
    }
}
