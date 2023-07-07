using AutoMapper;
using DataImporter.Common.Exceptions;
using DataImporter.Common.Utilities;
using DataImporter.ExcelFileReader;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.UnitOfWorks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly ITransferUnitOfWork _transferUnitOfWork;


        public GroupService(IMapper mapper,ITransferUnitOfWork transferUnitOfWork)
        {
            _mapper = mapper;
            _transferUnitOfWork = transferUnitOfWork;
        }
        public Home CountHomeProperty(Guid id)
        {
            
            var groupCount = _transferUnitOfWork.Groups.GetCount(x => x.UserId == id);
            return new Home()
            {
                Groups = groupCount
            };
        }
        //done
        public void CreateGroup(Group group)
        {
            if (group == null)
                throw new InvalidOperationException("course missing");

            if (group.GroupName == null)
                throw new InvalidOperationException("course name missing");

            _transferUnitOfWork.Groups.Add(
                _mapper.Map<Entities.Group>(group));
            _transferUnitOfWork.Save();
        }

        //needthismethod
        public (IList<ExcelFieldData> records, int total, int totalDisplay) GetAllData(int pageIndex, int pageSize, 
            string searchText, string sortText, int id, Guid userId)
        {
            var excelData = _transferUnitOfWork.ExcelDatas.Get(m => m.GroupId == id, "Group");
            var resultData = new List<ExcelFieldData>();
            foreach(var excel in excelData)
            {
                var excelFieldData = _transferUnitOfWork.ExcelFields.Get(m => m.ExcelDataId == excel.Id, "ExcelData");


                resultData = (from ED in excelData
                                   join EFD in excelFieldData on ED.Id equals EFD.ExcelDataId
                                   where ED.Id == EFD.ExcelDataId
                                   select _mapper.Map<ExcelFieldData>(EFD)).ToList();

            }




            return (resultData, resultData.Count, resultData.Count);
        }

      
        //done
        public Group GetGroup(int id)
        {
            var groupEntity = _transferUnitOfWork.Groups.GetById(id);

            if (groupEntity == null) return null;
            
            return _mapper.Map<Group>(groupEntity);
        }

        //done
        public (IList<Group> records, int total, int totalDisplay) GetGroupsByUserId(int pageIndex, 
            int pageSize, string searchText, string sortText, Guid id)
        {
            var groupData = _transferUnitOfWork.Groups.GetDynamic(
               string.IsNullOrWhiteSpace(searchText) ? x => x.UserId == id : x => x.GroupName.Contains(searchText),
               sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from groups in groupData.data
                              select _mapper.Map<Group>(groups)).ToList();

            return (resultData, groupData.total, groupData.totalDisplay);
        }

        //done
        public void GroupDelete(int id)
        {
            _transferUnitOfWork.Groups.Remove(id);
            _transferUnitOfWork.Save();
        }
        //done
        public IList<Group> LoadGroupProperty(Guid id)
        {
            var groupsList = new List<Group>();
            var groupEntity = _transferUnitOfWork.Groups.GetAll();

             groupsList = (from groups in groupEntity
                              where groups.UserId == id
                              select _mapper.Map<Group>(groups)).ToList();

            return groupsList;
        }

        public void UpdateGroup(Group group)
        {
            if (group == null)
                throw new InvalidOperationException("Group is missing");

            if (IsTitleAlreadyUsed(group.GroupName, group.Id))
                throw new DuplicateTitleException("Group title already used in other group.");

            var groupEntity = _transferUnitOfWork.Groups.GetById(group.Id);

            if (groupEntity != null)
            {
                _mapper.Map(group, groupEntity);
                _transferUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find group");
        }

        private bool IsTitleAlreadyUsed(string title) =>
          _transferUnitOfWork.Groups.GetCount(x => x.GroupName == title) > 0;

        private bool IsTitleAlreadyUsed(string title, int id) =>
            _transferUnitOfWork.Groups.GetCount(x => x.GroupName == title && x.Id != id) > 0;
    }
}
