using UrlShortener.Application.DTOs;

namespace UrlShortener.Application.ServiceAbstractions;

public interface IAuthenticationService
{
    public Task<Guid> RegisterUserAsync(CreateUserDto createUserDto); 
    
    public Task<GetUserSummaryDto> LoginUserAsync(LoginUserDto loginDto);
}