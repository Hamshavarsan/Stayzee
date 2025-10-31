 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; } = null;

        public string FilePath { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
