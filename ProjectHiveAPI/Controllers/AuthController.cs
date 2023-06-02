using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectHiveAPI.DataBase;
using ProjectHiveAPI.DI;
using ProjectHiveAPI.Models;
using ProjectHiveAPI.Services;

namespace ProjectHiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AuthModel authModel)
        {
            var user = await _authService.Login(authModel);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(AuthModel authModel)
        {

            // Создание нового пользователя и генерация JWT Token
            var user = new User
            {
                Login = authModel.Email,
                Email = authModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(authModel.Password),
                Role = "User",
                // Дополнительные поля пользователя
            };

            var existUser = await this._authService.Register(user);

            if (existUser)
            {
                return BadRequest("User with this email already exists");
            }

            var token = GenerateJwtToken(user);

            return Ok();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new byte[16]; // 16 байт (128 бит)
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, user.Email),

            // Дополнительные утверждения (claims) о пользователе
        }),
                Expires = DateTime.UtcNow.AddDays(7), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
