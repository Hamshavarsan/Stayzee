using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;


namespace StayZee.Application.Interfaces.Iservices
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO model);
        Task<AuthResponseDTO> LoginAsync(LoginDTO model);
        Task<string> ForgotPasswordAsync(ForgotPasswordRequestDTO model);
        Task<string> ResetPasswordAsync(ResetPasswordDTO model);

    }
}

