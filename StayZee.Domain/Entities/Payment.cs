using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public decimal Amount { get; set; }

        public string? StripeTxnId { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        public Invoice? Invoice { get; set; }
    }
}
