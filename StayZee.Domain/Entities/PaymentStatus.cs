using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class PaymentStatus
    {
    
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
