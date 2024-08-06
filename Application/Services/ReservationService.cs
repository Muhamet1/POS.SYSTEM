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
    public class ReservationService : IReservationService
    {
        private readonly IApplicationDbContext _context;
        public ReservationService (IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Reservation>> GetAllReservations()
        {
            return await _context.Reservations.ToListAsync();
        }
        public async Task<Reservation> GetReservation(int id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> CreateReservation(Reservation reservation)
        {
            var newReservation = new Reservation
            {
                ReservationName = reservation.ReservationName,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,  
                TableId = reservation.TableId
            };
            await _context.Reservations.AddAsync(newReservation);
            await _context.SaveChangesAsync();

            var findTable = _context.Tables.FirstOrDefault(x=>x.Id == reservation.TableId);
            findTable.Status = Core.Enums.TableTypes.Reserved;
            _context.Tables.Update(findTable);
            await _context.SaveChangesAsync();  
            return true;
        }
        public async Task<bool> DeleteReservation(int id)
        {
            var findReservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == id);
            if (findReservation != null)
            {
                _context.Reservations.Remove(findReservation);

                var findTable = _context.Tables.FirstOrDefault(x=> x.Id == findReservation.TableId);
                findTable.Status = Core.Enums.TableTypes.Avaiable;
                _context.Tables.Update(findTable);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
