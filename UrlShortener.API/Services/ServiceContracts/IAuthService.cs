using Microsoft.AspNetCore.Identity;
using Models.DTOs.UserDTOs;

namespace Services.ServiceContracts;

public interface IAuthService
{
    public Task<AuthResponse> LoginAsync(LoginUserDto loginUserDto);
    
    public Task<IdentityResult> RegisterAsync(RegisterUserDto registerUserDto);
}