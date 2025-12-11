using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NICOrPassport { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Admin", "Customer", "Rentals"
        public bool IsBlocked { get; set; } = false;
        public string? VerificationCode { get; set; }
        public DateTime? VerificationExpiresAt { get; set; }
        public bool IsVerified { get; set; } = false;
    }
}
