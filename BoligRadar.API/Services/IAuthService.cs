using BoligRadar.API.DTO;

namespace BoligRadar.API.Services;

public interface IAuthService
{
    Task<LoginResponseDto?> GoogleLoginAsync(string idToken);
    string GenerateJwtToken(int userId, string email);
}