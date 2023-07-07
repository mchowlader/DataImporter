using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Entities
{
    public class Export : IEntity<int>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Status { get; set; }
        public string GroupName { get; set; }
        public DateTime ExportDate { get; set; }
        public Group Group { get; set; }
    }
}
