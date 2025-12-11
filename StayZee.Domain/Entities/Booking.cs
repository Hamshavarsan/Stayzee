using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RentalId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Rental Rental { get; set; }
    }
}
