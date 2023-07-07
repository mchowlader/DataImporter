using AutoMapper;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.UnitOfWorks;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public class ImportService : IImportService
    {
        private readonly IMapper _mapper;
        private readonly ITransferUnitOfWork _transferUnitOfWork;

        public ImportService(IMapper mapper, ITransferUnitOfWork transferUnitOfWork)
        {
            _mapper = mapper;
            _transferUnitOfWork = transferUnitOfWork;
        }

        public void DeleteDoneItem()
        {
            var importEntity = _transferUnitOfWork.Imports.Get(m => m.Status == "Done", "Group");

            foreach(var import in importEntity)
            {
                if (import != null)
                    File.Delete(import.FilePath);
            }
        }

        public (IList<Import> records, int total, int totalDisplay) GetImportsData(int pageIndex,
            int pageSize, string searchText, string sortText, Guid id)
        {
            var importsData = _transferUnitOfWork.Imports.GetDynamic(
               string.IsNullOrWhiteSpace(searchText) ? null : x => x.GroupName.Contains(searchText),
               sortText, string.Empty, pageIndex, pageSize);

            var groupData = _transferUnitOfWork.Groups.GetDynamic(
               string.IsNullOrWhiteSpace(searchText) ? null : x => x.UserId.ToString().Contains(searchText),
               sortText, string.Empty, pageIndex, pageSize);

            var resultData = new List<Import>();
            foreach(var  group in groupData.data)
            {
                foreach(var import in  importsData.data)
                {
                    if(group.UserId == id && import.GroupId == group.Id)
                    {
                        var importData = _mapper.Map<Import>(import);
                        resultData.Add(importData);
                    }
                }
            }

            return (resultData, importsData.total, importsData.totalDisplay);
        }

        public List<Import> GetPendingItem()
        {
            List<Import> pendingItemList = new List<Import>();
            var importEntity = _transferUnitOfWork.Imports.Get(m => m.Status.Contains("Pending"), "Group");

            foreach(var imports in importEntity)
            {
                var import = _mapper.Map<Import>(imports);
                pendingItemList.Add(import);
            }

            return pendingItemList;
        }

        public void StatusUpdate(int groupId)
        {
            var importEntity = _transferUnitOfWork.Imports.Get(m => m.GroupId == groupId, "Group");
            if (importEntity != null)
            {
                foreach(var import in importEntity)
                {
                    import.Status = "Processing";

                }
                _transferUnitOfWork.Save();
            }
        }

        public void UpdateProcessStatus(int groupId)
        {
            var importEntity = _transferUnitOfWork.Imports.Get(m => m.GroupId == groupId, "Group");
            if (importEntity != null)
            {
                foreach (var import in importEntity)
                {
                    import.Status = "Done";

                }
                _transferUnitOfWork.Save();
            }
        }

        public void UploadExcelFile(Import importsData)
        {
            var groupEntity = _transferUnitOfWork.Groups.GetById(importsData.GroupId);
            _transferUnitOfWork.Imports.Add(
                new Entities.Import()
                {
                    Status = importsData.Status,
                    GroupId = importsData.GroupId,
                    FilePath = importsData.FilePath,
                    GroupName = groupEntity.GroupName,
                    ImportDate = importsData.ImportDate,
                    ExcelFileName = importsData.ExcelFileName

                });
            _transferUnitOfWork.Save();
        }
    }
}
