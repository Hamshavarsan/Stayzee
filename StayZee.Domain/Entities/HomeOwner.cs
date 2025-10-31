using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class HomeOwner
    {
        public Guid HomeOwnerID { get; set; }
        public string Name { get; set; }
        public int ContactNo { get; set; }
    }
}
