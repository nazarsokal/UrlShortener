using Microsoft.AspNetCore.Identity;

namespace Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public ICollection<ShortenUrl> ShortenedUrls { get; set; } = new List<ShortenUrl>();
}