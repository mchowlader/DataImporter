using DataImporter.Data;
using DataImporter.User.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get ; set ; }
        public string GroupName { get ; set ; }
        public DateTime CreateDate { get; set; }
        public List<Export> Exports { get; set; }
        public List<Import> Imports { get; set; }
        public List<ColumnData> ColumnDatas { get; set; }
        public Guid UserId { get; set; }
        public List<ExcelData> ExcelDatas { get; set; }
        public ApplicationUser User { get; set; }


    }
}
