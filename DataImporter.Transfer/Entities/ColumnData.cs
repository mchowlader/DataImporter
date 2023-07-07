using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.Entities
{
    public class ColumnData : IEntity<int>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnNumber { get; set; }
        public Group Group { get; set; }

    }
}
