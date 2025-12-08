namespace StayZee.Application.DTOs.RequestDTO
{
    public class BookingShareRequestDto
    {
        public Guid BookingId { get; set; }
        public List<string> Emails { get; set; } = new();
    }
}
