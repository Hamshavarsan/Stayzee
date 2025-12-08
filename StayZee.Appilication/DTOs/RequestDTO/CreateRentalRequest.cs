using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StayZee.Application.DTOs.RequestDTO
{
    public class CreateRentalRequest
    {
        public int UserId { get; set; }
        public string HomeTitle { get; set; }
        public string AccountNumber { get; set; }
        public string HomeLocation { get; set; }
        public int Bedrooms { get; set; }
        public bool PetFriendly { get; set; }
        public decimal OneDayPrice { get; set; }
        public decimal MonthPrice { get; set; }
        public decimal CurrentBill { get; set; }

        public List<IFormFile> Photos { get; set; }
    }
}
