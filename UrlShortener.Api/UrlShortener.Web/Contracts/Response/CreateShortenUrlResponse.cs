namespace UrlShortener.Web.Contracts.Response;

public class CreateShortenUrlResponse
{
    public Guid Id { get; set; }
    
    public required string UrlOriginal { get; set; }
    
    public required string UrlShortened { get; set; }
    
    public DateTime DateCreated { get; set; }
}

