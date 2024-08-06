using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public TableTypes Status { get; set; }    
    }
}
