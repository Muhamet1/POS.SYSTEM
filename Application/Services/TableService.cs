using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.DTOs;

namespace Application.Services
{
    public class TableService : IITableService
    {
        private readonly IApplicationDbContext _context;
        public TableService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TableDTO>> GetAllTables()
        {
            var tables = await _context.Tables.ToListAsync();
            var newDto = new List<TableDTO>();
            foreach (var table in tables)
            {
                var dto = new TableDTO();
                dto.Status = table.Status;
                dto.Id = table.Id;
                dto.Name = table.Name;
                dto.Number = table.Number;
                var findReservationTables = _context.Reservations.Where(x=>x.TableId == table.Id && x.StartTime.Value.Date == DateTime.UtcNow.Date).FirstOrDefault();

                if(findReservationTables != null && table.Status == Core.Enums.TableTypes.Reserved)
                {
                    var startTime = findReservationTables.StartTime ?? null;
                    var endTime = findReservationTables.EndTime ?? null;
                    dto.ReservationInfo = "Reserved from " + startTime.Value.ToString("HH:mm") + " to " + endTime.Value.ToString("HH:mm");
                    dto.ReservationStartTime = findReservationTables.StartTime;
                    dto.ReservationEndTime = findReservationTables.EndTime;
                }
                newDto.Add(dto);
            }
            return newDto;
        }
        public Task<List<Table>> GetAvailableTables()
        {
            return _context.Tables.ToListAsync();
        }
        public async Task<bool> AddTable(Table table)
        {
            var newItem = new Table { Number = table.Number, Name = table.Name, Status = Core.Enums.TableTypes.Avaiable };
            _context.Tables.Add(newItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveTable(int tableId)
        {
            var findTable = _context.Tables.FirstOrDefault(x => x.Id == tableId);
            if (findTable != null)
            {
                _context.Tables.Remove(findTable);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> ChangeTableStatus(int tableId)
        {
            var findTable = await _context.Tables.FirstOrDefaultAsync(x => x.Id == tableId);
            if (findTable != null)
            {
                findTable.Status = Core.Enums.TableTypes.Avaiable;
                _context.Tables.Update(findTable);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<OrderDTO?> GetOrderByTableId(int tableId)
        {
            var findOrderByTableId = _context.Orders.Where(x => x.TableId == tableId).OrderByDescending(x=>x.Id).FirstOrDefault();
            if (findOrderByTableId != null)
            {
                var dto = new OrderDTO();
                var findItemName = _context.Orders.Where(x => x.TableId == findOrderByTableId.TableId && x.Total == findOrderByTableId.Total).Select(x=>x.Item).ToList();
                var findTableNumber = _context.Tables.Where(x => x.Id == findOrderByTableId.TableId).Select(x => x.Number).FirstOrDefault();
                dto.Items = findItemName;
                dto.TableNumber = findTableNumber;
                dto.OrderName = findOrderByTableId.OrderName;
                dto.OrderDescription = findOrderByTableId.OrderDescription;
                dto.Id = findOrderByTableId.Id;
                dto.Total = findOrderByTableId.Total;

                return dto;
            }
            return null;
        }
    }
}
