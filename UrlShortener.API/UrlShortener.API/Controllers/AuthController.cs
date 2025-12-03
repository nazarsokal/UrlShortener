using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.UserDTOs;
using Services.ServiceContracts;

namespace UrlShortener.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto request)
    {
        AuthResponse? loginResult = await _authService.LoginAsync(request).ConfigureAwait(false);
        
        if(loginResult == null)
            return Unauthorized("Invalid Login or Password");
        
        return Ok(loginResult);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {
        var registrationResult = await _authService.RegisterAsync(request);
        
        if (registrationResult.Succeeded)
        {
            return Ok("User registered successfully.");
        }
        
        return BadRequest(registrationResult.Errors);
    }
}