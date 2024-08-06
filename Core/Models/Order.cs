using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? OrderName { get; set; }
        public string? OrderDescription { get; set; }
        public double Total { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public Table Table { get; set; }
        public int TableId { get; set; }    
    }
}
