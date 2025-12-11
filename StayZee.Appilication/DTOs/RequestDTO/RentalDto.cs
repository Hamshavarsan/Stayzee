using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Application.DTOs.RequestDTO
{
    public class RentalDto
    {
        public int Id { get; set; }
        public string HomeTitle { get; set; } = string.Empty;
        public string HomeLocation { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public bool PetFriendly { get; set; }
        public decimal OneDayPrice { get; set; }
        public decimal MonthPrice { get; set; }
        public List<string> PhotoUrls { get; set; } = new List<string>();
    }
}
