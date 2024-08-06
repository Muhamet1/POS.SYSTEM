using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string? ReservationName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Table Table { get; set; }    
        public int TableId { get; set; }
    }
}
