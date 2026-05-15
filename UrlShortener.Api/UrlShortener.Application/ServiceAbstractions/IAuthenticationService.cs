using UrlShortener.Application.DTOs;

namespace UrlShortener.Application.ServiceAbstractions;

public interface IAuthenticationService
{
    public Task<AuthUserDto> RegisterUserAsync(CreateUserDto createUserDto); 
    
    public Task<AuthUserDto> LoginUserAsync(LoginUserDto loginDto);
}