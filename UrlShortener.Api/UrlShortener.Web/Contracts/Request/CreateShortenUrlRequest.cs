namespace UrlShortener.Web.Contracts.Request;

public class CreateShortenUrlRequest
{
    public required string OriginalUrl { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }
}