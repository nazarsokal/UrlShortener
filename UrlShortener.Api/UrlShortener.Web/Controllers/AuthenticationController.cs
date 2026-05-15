using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.DTOs;
using UrlShortener.Application.ServiceAbstractions;
using UrlShortener.Web.Contracts.Request;

namespace UrlShortener.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;
    private readonly IMapper mapper;

    public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
    {
        this.authenticationService = authenticationService;
        this.mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] PostUserRequest request)
    {
        var userDto = this.mapper.Map<LoginUserDto>(request);
        
        var foundUser = await this.authenticationService.LoginUserAsync(userDto);
        return this.Ok(foundUser);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PostUserRequest request)
    {
        var userDto = this.mapper.Map<CreateUserDto>(request);
        
        var foundUser = await this.authenticationService.RegisterUserAsync(userDto);
        return this.Ok(foundUser);
    }
}