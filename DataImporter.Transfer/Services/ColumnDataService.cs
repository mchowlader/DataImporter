using AutoMapper;
using DataImporter.Transfer.BusinessObjects;
using DataImporter.Transfer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Services
{
    public class ColumnDataService : IColumnDataService
    {
        private readonly IMapper _mapper;
        private readonly ITransferUnitOfWork _transferUnitOfWork;


        public ColumnDataService(IMapper mapper, ITransferUnitOfWork transferUnitOfWork)
        {
            _mapper = mapper;
            _transferUnitOfWork = transferUnitOfWork;
        }

        public List<string> FileMatching(int groupId)
        {
            if(ExitGroupId(groupId))
            {
                List<string> columnList = new List<string>();
                var columnEntity = _transferUnitOfWork.ColumnDatas.Get(x => x.GroupId == groupId, "Group");

                foreach (var name in columnEntity)
                {
                    columnList.Add(name.ColumnName);
                }
                return columnList;
            }

            else
            {
                return null;
            }
            
        }

        public List<ColumnData> GetColumnDataById(int groupId)
        {

            List<ColumnData> ColumnDataList = new List<ColumnData>();
            var colmunDataEntity = _transferUnitOfWork.ColumnDatas.Get(m => m.GroupId == groupId, "Group");

            foreach(var item in colmunDataEntity)
            {
                var columnDataObj = _mapper.Map<ColumnData>(item);
                //var columnDataObj = new ColumnData()
                //{ 
                //    ColumnName = item.ColumnName,
                //    ColumnNumber = item.ColumnNumber,
                //    GroupId = item.GroupId
                
                //};
                ColumnDataList.Add(columnDataObj);
            }

            return ColumnDataList;
        }

        public void InsertColumnHeader(ColumnData column)
        {
            var x = new Entities.ColumnData()
            {
                ColumnName = column.ColumnName,
                ColumnNumber = column.ColumnNumber,
                GroupId = column.GroupId
            };

            _transferUnitOfWork.ColumnDatas.Add(x);
            //_mapper.Map<Entities.ColumnData>(column)
            _transferUnitOfWork.Save();
        }

        private bool ExitGroupId(int groupId) => 
            _transferUnitOfWork.ColumnDatas.GetCount(m => m.GroupId == groupId) <= 0 ? false : true ;
    }
}
