using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Application.DTOs.RequestDTO
{
    public class ForgotPasswordRequestDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}

