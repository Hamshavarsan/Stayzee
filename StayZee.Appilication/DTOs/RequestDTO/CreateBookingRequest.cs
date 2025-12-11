using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Application.DTOs.RequestDTO
{
    public class CreateBookingRequest
    {
        public int RentalId { get; set; }        
        public string UserId { get; set; }       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
