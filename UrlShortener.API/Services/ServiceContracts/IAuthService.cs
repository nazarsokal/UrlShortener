using Microsoft.AspNetCore.Identity;

namespace Services.ServiceContracts;

public interface IAuthService
{
    public Task<SignInResult> LoginAsync(string username, string password);
    
    public Task<IdentityResult> RegisterAsync(string username, string email, string password);
}