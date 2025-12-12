using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StayZee.Application.DTOs.ResponseDTO
{
    public class RentalResponseDTO
    {
        public int RentalId { get; set; }
        public int Id { get; set; }
        public string HomeTitle { get; set; }
        public string HomeLocation { get; set; }
        public int Bedrooms { get; set; }
        public bool PetFriendly { get; set; }
        public decimal OneDayPrice { get; set; }
        public decimal MonthPrice { get; set; }
        public List<string> PhotoUrls { get; set; }
        // public string Message { get; set; }
    }
}

