using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TableDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public TableTypes Status { get; set; }
        public string? ReservationInfo { get; set; } = "no reservation available";
        public DateTime? ReservationStartTime{ get; set; }
        public DateTime? ReservationEndTime{ get; set; }
    }
}
