using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Item> Items { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Sale> Sales { get; set; }
        DbSet<Table> Tables { get; set; }   
        DbSet<Order> Orders { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        Task<int> SaveChangesAsync();
    }
}
