using AuthSite.Models;
using LibAppBase.Configuration;
using LibAppBase.Models;
using LibAppBase.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthSite.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private const string TestUsername = "admin";
    private const string TestPassword = "password123";

    private readonly JwtTokenGenerator _tokenGenerator;
    private readonly JwtSettings _jwtSettings;

    public AuthController(JwtTokenGenerator tokenGenerator, IOptions<JwtSettings> jwtOptions)
    {
        _tokenGenerator = tokenGenerator;
        _jwtSettings = jwtOptions.Value;
    }

    [HttpPost("login")]
    public ActionResult<ApiResponse<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (request.Username != TestUsername || request.Password != TestPassword)
        {
            return Unauthorized(ApiResponse<LoginResponse>.Fail("Invalid username or password."));
        }

        var token = _tokenGenerator.GenerateToken(request.Username);
        var response = new LoginResponse
        {
            Token = token,
            ExpiresInMinutes = _jwtSettings.ExpirationMinutes
        };

        return Ok(ApiResponse<LoginResponse>.Ok(response, "Login successful."));
    }
}
