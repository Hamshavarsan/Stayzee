using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class KYC
    {
        public Guid KYCId { get; set; }
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
        public string DocumentType { get; set; } = null;
        public string FilePath { get; set; } = null;
        public string Status { get; set; } = "Pending";
        public DateTime UploadAt { get; set; } = DateTime.UtcNow;
        public Customer? Customer { get; set; }

    }
}
