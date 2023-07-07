using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public interface IGroupService
    {
        void CreateGroup(Group group);
        (IList<Group> records, int total, int totalDisplay) GetGroupsByUserId(int pageIndex, 
            int pageSize, string searchText, string sortText,Guid id);
        Home CountHomeProperty(Guid id);
        void GroupDelete(int id);
        Group GetGroup(int id);
        IList<Group> LoadGroupProperty(Guid id); //cng
        void UpdateGroup(Group group);
        (IList<ExcelFieldData> records, int total, int totalDisplay) GetAllData(int pageIndex, int pageSize, 
            string searchText, string sortText, int id, Guid userId);

    }
}
