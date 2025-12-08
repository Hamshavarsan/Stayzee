using System;

namespace StayZee.Domain.Entities
{
    public class Booking
    {
        public Guid BookingId { get; set; } = Guid.NewGuid();
        public Guid PropertyId { get; set; }
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public Guid HomeId { get; set; }
        public Home? Home { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid BookingStatusId { get; set; }
        public BookingStatus? BookingStatus { get; set; }
        public Guid PaymentStatusId { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public Payment? Payment { get; set; }
        public Invoice? Invoice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Fix non-nullable property by initializing
        public string Status { get; set; } = string.Empty;

        // Shared booking
        public string? SharedEmails { get; set; }
        public DateTime? SharedAt { get; set; }
    }
}



