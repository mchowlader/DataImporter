using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Transfer.BusinessObjects
{
    public class Group 
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }


    }
}
