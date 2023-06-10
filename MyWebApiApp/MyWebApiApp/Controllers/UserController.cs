using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSetting  _appSettings;

        public UserController(MyDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }
        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.NguoiDungs.SingleOrDefault(p => p.UserName == model.UserName 
            && model.Password == p.Password);
            if(user ==null) // không đúng 
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username/password"
                });
            }

            // cấp token

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate succsess",
                Data = GenerateToken(user)
            }); ;
        }
        
        private async  Task<TokenModel> GenerateToken(NguoiDung nguoiDung)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,nguoiDung.HoTen),
                    new Claim(JwtRegisteredClaimNames.Email,nguoiDung.Email),
                    new Claim(JwtRegisteredClaimNames.Sub,nguoiDung.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("UserNam", nguoiDung.UserName),
                    new Claim("Id", nguoiDung.Id.ToString()),

                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                SecurityAlgorithms.HmacSha512Signature)
             
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            // Lưu vào db
            var refeshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtId= token.Id,
                Token= refreshToken,
                IsUsed =false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };

            await _context.AddAsync(refeshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = GenerateRefreshToken()
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random); 
            }
        }
    }
}
