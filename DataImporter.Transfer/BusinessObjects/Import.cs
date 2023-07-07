using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.BusinessObjects
{
    public class Import 
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Status { get; set; }
        public string ExcelFileName { get; set; }
        public DateTime ImportDate { get; set; }
        public string FilePath { get; set; }

    }
}
