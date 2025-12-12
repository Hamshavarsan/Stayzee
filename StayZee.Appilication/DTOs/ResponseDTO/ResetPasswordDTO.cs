using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Application.DTOs.RequestDTO
{
    public class ResetPasswordDTO
    {
        public string Username { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}

