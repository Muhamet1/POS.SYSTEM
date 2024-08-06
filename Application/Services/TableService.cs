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
    }
}
