using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class Home
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null;

        public string Description { get; set; }
        public string Address { get; set; }
        public string? ImagePath { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string features { get; set; }
        public decimal RatePerDay { get; set; }


        public Guid OwnerId { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<HomeDocument> Documents { get; set; } = new List<HomeDocument>();

        public Guid HomeApprovalStatusId { get; set; }
        public Home homeApprovalStatus { get; set; } = null;

    }
}
