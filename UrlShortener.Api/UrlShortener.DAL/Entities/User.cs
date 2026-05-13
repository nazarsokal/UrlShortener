using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DAL.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public required string Username { get; set; }
    
    public required string PasswordHash { get; set; }

    public DateTime DateJoined { get; set; }
    
    public ICollection<ShortenUrl> ShortenUrls { get; set; } = new List<ShortenUrl>();
}