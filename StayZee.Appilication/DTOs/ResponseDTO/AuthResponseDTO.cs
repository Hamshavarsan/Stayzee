using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Application.DTOs.ResponseDTO
{
    public class AuthResponseDTO
    {
        public string? Token { get; set; }
        public string? Message { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
    }
}
