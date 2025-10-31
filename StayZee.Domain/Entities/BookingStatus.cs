using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class BookingStatus
    {
        public Guid BookingStatusId { get; set; }
        public string BookingStatusName { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
    

