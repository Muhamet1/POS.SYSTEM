﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string? OrderName { get; set; }
        public string? OrderDescription { get; set; }
        public double Total { get; set; }
        public List<Item>? Items { get; set; }
        public int TableNumber { get; set; }
    }
}
