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

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO model)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(model.Username);
            if (existingUser != null)
                throw new Exception("Username already exists!");

            var newUser = new User
            {
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                NICOrPassport = model.NICOrPassport,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = model.Role
            };

            await _userRepository.AddUserAsync(newUser);
            return await GenerateToken(newUser); // இங்க call ஆகுது
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
    }
}

