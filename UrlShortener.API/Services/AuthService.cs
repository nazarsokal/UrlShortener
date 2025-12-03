using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Models;
using Models.DTOs.UserDTOs;
using Services.Helpers;
using Services.ServiceContracts;

namespace Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task<AuthResponse> LoginAsync(LoginUserDto loginUserDto)
    {
        var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
        if (user == null)
            throw new ApplicationException("Invalid username or password");
        
        var valid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
        
        if (!valid)
            throw new ApplicationException("Invalid username or password");
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var token = JwtHelper.GenerateJwtToken(user, roles, _configuration);

        var userDtoToReturn =  new ReturnUserDto()
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Roles = roles
        };

        return new AuthResponse()
        {
            Token = token,
            ReturnUserDto = userDtoToReturn
        };
    }

    public async Task<IdentityResult> RegisterAsync(RegisterUserDto registerUserDto)
    {
        var user = new ApplicationUser{
            UserName = registerUserDto.UserName,
            Email = registerUserDto.Email,
            PasswordHash = registerUserDto.Password,
        };
        
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        
        if(!result.Succeeded)
            throw new ApplicationException($"Failed to register user: {string.Join(',', result.Errors)}");
        
        await _userManager.AddToRoleAsync(user, "User");
        
        return result;
    }
}