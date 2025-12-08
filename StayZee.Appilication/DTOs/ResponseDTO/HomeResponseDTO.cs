
namespace StayZee.Application.DTOs.ResponseDTO

{
    public class HomeResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string Features { get; set; } = string.Empty;
        public decimal RatePerDay { get; set; }
        public Guid OwnerId { get; set; }
        public string? ApprovalStatus { get; set; }
    }
}
