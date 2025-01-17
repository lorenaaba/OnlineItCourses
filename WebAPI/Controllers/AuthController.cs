using Microsoft.AspNetCore.Mvc;
using OnlineITCourses.Dto;
using OnlineITCourses.Helper;

namespace OnlineITCourses.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = await _authService.RegisterAsync(request.Username, request.Password, "User");
        return Ok(new { Message = "Registracija uspješna", User = user.Username });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.AuthenticateAsync(request.Username, request.Password);
        if (user == null)
        {
            return Unauthorized(new { Message = "Neispravni podaci za prijavu" });
        }

        var token = JwtHelper.GenerateToken(user, _configuration["Jwt:Key"], int.Parse(_configuration["Jwt:ExpiryMinutes"]));
        return Ok(new { Token = token });
    }

    [HttpPost("changepassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var user = await _authService.AuthenticateAsync(request.Username, request.OldPassword);
        if (user == null)
        {
            return Unauthorized(new { Message = "Neispravni podaci za prijavu" });
        }

        user.PasswordHash = _authService.RegisterAsync(request.Username, request.NewPassword, user.Role).Result.PasswordHash;
        await _authService.RegisterAsync(user.Username, request.NewPassword, user.Role);
        return Ok(new { Message = "Lozinka uspješno promijenjena" });
    }
}