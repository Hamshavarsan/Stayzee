using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class BookingSharedCustomer
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

