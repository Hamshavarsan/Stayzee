using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class HomeApprovalStatus
    {
        public Guid HomeApprovalStatusId { get; set; }
        public string Name { get; set; } // pending, apporaved, rejected

        public ICollection<Home> homes { get; set; } = new List<Home>();

    }
}
