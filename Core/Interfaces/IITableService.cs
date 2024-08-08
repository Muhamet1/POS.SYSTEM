using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Models;

namespace Core.Interfaces
{
    public interface IITableService
    {
        Task<List<TableDTO>> GetAllTables();
        Task<List<Table>> GetAvailableTables();
        Task<bool> AddTable(Table table);
        Task<bool> RemoveTable(int tableId);
        Task<bool> ChangeTableStatus(int tableId);
        Task<OrderDTO?> GetOrderByTableId(int tableId);
    }
}
