using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }   
    }
}
