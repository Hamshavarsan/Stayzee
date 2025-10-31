using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class HomeDocument
    {
        public Guid HomeDocumentId { get; set; }
        public Guid HomeId { get; set; }
        public Home home { get; set; } = null;
        public string DocumentType { get; set; } = null;
        public string FilePath { get; set; } = null;
        public DateTime UploadAt { get; set; } = DateTime.UtcNow;
    }
}
