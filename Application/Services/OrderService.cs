using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApplicationDbContext _context;
        public OrderService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderDTO>> GetAllOrders()
        {
            var result = await _context.Orders.ToListAsync();
            var newDto = new List<OrderDTO>();
            foreach (var order in result)
            {
                var dto = new OrderDTO();
                var findItemName = _context.Items.Where(x => x.Id == order.ItemId).ToList();
                var findTableNumber = _context.Tables.Where(x=>x.Id == order.TableId).Select(x=>x.Number).FirstOrDefault();
                dto.Items = findItemName;
                dto.TableNumber = findTableNumber;
                dto.OrderName = order.OrderName;
                dto.OrderDescription = order.OrderDescription; 
                dto.Id = order.Id;  
                dto.Total = order.Total;
                newDto.Add(dto);
            }
            return newDto;  
        }
        public async Task<OrderDTO> GetOrder(int id)
        {
            var order = await _context.Orders.Where(x => x.Id == id).FirstAsync();
            var dto = new OrderDTO();
            var findItemName = _context.Items.Where(x => x.Id == order.ItemId).ToList();
            var findTableNumber = _context.Tables.Where(x => x.Id == order.TableId).Select(x => x.Number).FirstOrDefault();
            dto.Items = findItemName;
            dto.TableNumber = findTableNumber;
            dto.OrderName = order.OrderName;
            dto.OrderDescription = order.OrderDescription;
            dto.Id = order.Id;
            dto.Total = order.Total;
            
            return dto;
        }
        public async Task<bool> CreateOrder(int tableId, int itemId, double total)
        {
            var newOrder = new Order { ItemId = itemId, TableId = tableId, OrderName = "order", OrderDescription = "desc", Total = total };
            await _context.Orders.AddAsync(newOrder);
            var findTable = _context.Tables.Where(x=>x.Id == tableId).FirstOrDefault();
            findTable.Status = Core.Enums.TableTypes.Busy;
            _context.Tables.Update(findTable);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteOrder(int id)
        {
            var findOrderFromDb = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (findOrderFromDb != null)
            {
                _context.Orders.Remove(findOrderFromDb);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
