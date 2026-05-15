namespace UrlShortener.Web.Contracts.Response;

public class AuthResponse
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string AccessToken { get; set; }
}