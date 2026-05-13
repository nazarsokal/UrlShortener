namespace UrlShortener.DAL.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public required string Username { get; set; }
    
    public required string PasswordHash { get; set; }

    public DateTime DateJoined { get; set; }
}