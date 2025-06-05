using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BoligRadar.API.Data;
using BoligRadar.API.DTO;
using BoligRadar.API.Models;

namespace BoligRadar.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        public async Task<LoginResponseDto?> GoogleLoginAsync(string idToken)
        {
            try
            {
                // Verify Google token
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["GoogleAuth:ClientId"] }
                };
                
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                
                // Check if user exists
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.GoogleId == payload.Subject);
                
                if (user == null)
                {
                    // Create new user
                    user = new User
                    {
                        GoogleId = payload.Subject,
                        Email = payload.Email,
                        Name = payload.Name ?? payload.Email,
                        CreatedAt = DateTime.UtcNow
                    };
                    
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                
                // Generate JWT token
                var token = GenerateJwtToken(user.Id, user.Email);
                
                return new LoginResponseDto
                {
                    Token = token,
                    Email = user.Email,
                    Name = user.Name
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public string GenerateJwtToken(int userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtSettings:ExpiryDays"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}