using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllReservations();
        Task<Reservation> GetReservation(int id);
        Task<bool> CreateReservation (Reservation reservation);
        Task<bool> DeleteReservation (int id);
    }
}
