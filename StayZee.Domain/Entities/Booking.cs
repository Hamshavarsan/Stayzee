using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class Booking
    {
        public Guid BookingId { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null;

        public Guid HomeId { get; set; }
        public Home Home { get; set; } = null;

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }

        public Guid BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; } = null;

        public Guid PaymentStatusId { get; set; }
        public BookingStatus PaymentStatus { get; set; } = null;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Payment? Payment { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
