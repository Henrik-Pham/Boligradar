using BoligRadar.API.DTO;
using BoligRadar.API.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BoligRadar.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
    {
        var result = await _authService.GoogleLoginAsync(googleLoginDto.IdToken);

        if (result == null)
        {
            return Unauthorized(new { message = "Login failed" });
        }

        return Ok(result);
    }
}