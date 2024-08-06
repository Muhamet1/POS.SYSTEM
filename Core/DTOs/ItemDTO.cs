using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public int CounterSameItem { get; set; } = 1;
        public double? Total_By_Item { get; set; }

        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
