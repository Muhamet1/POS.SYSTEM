using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? SaleDate { get; set; }
        public double? Price { get; set; }
        public string? SalesName { get; set; }
        public PaymentType PaymentType { get; set; }
        public ICollection<Item> InventoryItems { get; set; }
    }
}
