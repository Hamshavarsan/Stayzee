namespace StayZee.Application.DTOs.RequestDTO
{
    public class BookingRequestDto
    {
        public Guid CustomerId { get; set; }
        public Guid HomeId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid BookingStatusId { get; set; }
        
    }
}
