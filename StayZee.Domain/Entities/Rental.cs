namespace StayZee.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK

        public string HomeTitle { get; set; }
        public decimal CurrentBill { get; set; }
        public string HomeLocation { get; set; }
        public int Bedrooms { get; set; }
        public bool PetFriendly { get; set; }
        public decimal OneDayPrice { get; set; }
        public decimal MonthPrice { get; set; }
          

        public string PhotoUrl1 { get; set; }
        public string PhotoUrl2 { get; set; }
        public string PhotoUrl3 { get; set; }
        public string PhotoUrl4 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
    }
}
