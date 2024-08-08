using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrders();
        Task<OrderDTO> GetOrder(int id);
        Task<bool> CreateOrder(int tableId, int itemId, double total);
        Task<bool> DeleteOrder(int id);
    }
}
