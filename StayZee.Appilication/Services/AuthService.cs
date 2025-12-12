using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.Interfaces.IRepository;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public AuthService(IUserRepository userRepository, IConfiguration config, IEmailService emailService)
        {
            _userRepository = userRepository;
            _config = config;
            _emailService = emailService;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO model)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(model.Username);
            
            if (existingUser != null)
            {
                if (existingUser.IsVerified)
                {
                    throw new Exception("Username already exists!");
                }
                
                // If user exists but not verified, resend code
                var newCode = new Random().Next(100000, 999999).ToString();
                existingUser.VerificationCode = newCode;
                existingUser.VerificationExpiresAt = DateTime.UtcNow.AddMinutes(15);
                
                // Optionally update password if needed, but for now just update code
                 existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password); // Ensure they can login with new pass
                 existingUser.Email = model.Email; // Allow correcting email

                await _userRepository.UpdateUserAsync(existingUser);
                
                await _emailService.SendEmailAsync(
                    existingUser.Email, 
                    "StayZee - Verify Your Email", 
                    $"Your verification code is: <b>{newCode}</b>. It expires in 15 minutes."
                );
                
                return new AuthResponseDTO 
                { 
                    Message = "Verification code resent to your email.", 
                    Username = existingUser.Username, 
                    Role = existingUser.Role 
                };
            }

            var verificationCode = new Random().Next(100000, 999999).ToString();

            var newUser = new User
            {
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                NICOrPassport = model.NICOrPassport,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = model.Role,
                VerificationCode = verificationCode,
                VerificationExpiresAt = DateTime.UtcNow.AddMinutes(15),
                IsVerified = false
            };

            await _userRepository.AddUserAsync(newUser);

            await _emailService.SendEmailAsync(
                newUser.Email, 
                "StayZee - Verify Your Email", 
                $"Your verification code is: <b>{verificationCode}</b>. It expires in 15 minutes."
            );

            return new AuthResponseDTO 
            { 
                Message = "Registration successful. Please check your email for verification code.", 
                Username = newUser.Username, 
                Role = newUser.Role 
            };
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await _userRepository.GetByUsernameAsync(model.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return await GenerateToken(user); // இங்கயும் call ஆகுது
        }


        // இதுதான் நீ தேடுனது – GenerateToken method இங்கதான் இருக்கணும்!
        private async Task<AuthResponseDTO> GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // userId JWT-லயும் போகுது
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.Username,
                Role = user.Role,
                UserId = user.Id                     // இதுதான் Angular-க்கு வரும்!
            };
        }
        public async Task<string> ForgotPasswordAsync(ForgotPasswordRequestDTO model)
        {
            var user = await _userRepository.GetByUsernameAsync(model.Username);

            if (user == null || user.Email != model.Email)
                throw new Exception("Invalid username or email");

            var code = new Random().Next(100000, 999999).ToString();

            user.VerificationCode = code;
            user.VerificationExpiresAt = DateTime.UtcNow.AddMinutes(15);

            await _userRepository.UpdateUserAsync(user);

            await _emailService.SendEmailAsync(
                user.Email,
                "Password Reset Code - StayZee",
                $"Your password reset code is <b>{code}</b>. It expires in 15 minutes."
            );

            return "Password reset code sent to your email.";
        }
        public async Task<string> ResetPasswordAsync(ResetPasswordDTO model)
        {
            var user = await _userRepository.GetByUsernameAsync(model.Username);

            if (user == null)
                throw new Exception("User not found");

            if (user.VerificationCode != model.Code ||
                user.VerificationExpiresAt < DateTime.UtcNow)
                throw new Exception("Invalid or expired code");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.VerificationCode = null;
            user.VerificationExpiresAt = null;

            await _userRepository.UpdateUserAsync(user);

            return "Password reset successful.";
        }



    }
}

