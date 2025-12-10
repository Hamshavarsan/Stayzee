namespace StayZee.Application.DTOs.ResponseDTO
{
    public class BookingResponseDto
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid HomeId { get; set; }
        public string? HomeName { get; set; }
        public List<string>? HomeImages { get; set; } // new
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? BookingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Guid>? SharedCustomerIds { get; set; }
        public List<string>? SharedCustomerEmails { get; set; }
    }
}
