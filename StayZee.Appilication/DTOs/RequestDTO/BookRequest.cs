using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Application.DTOs.RequestDTO
{
    public class BookRequest
    {
        public int RentalId { get; set; }
        public int UserId { get; set; }
    }
}
