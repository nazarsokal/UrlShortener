namespace UrlShortener.Application.DTOs;

public class AuthUserDto
{
    public Guid Id { get; set; }
    
    public required string Username { get; set; }
    
    public string AccessToken { get; set; }
}