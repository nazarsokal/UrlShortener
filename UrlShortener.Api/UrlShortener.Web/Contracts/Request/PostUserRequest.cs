namespace UrlShortener.Web.Contracts.Request;

public class PostUserRequest
{
    public required string Username { get; set; }
    
    public required string Password { get; set; }
}