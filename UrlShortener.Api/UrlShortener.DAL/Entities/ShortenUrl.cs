namespace UrlShortener.DAL.Entities;

public class ShortenUrl
{
    public Guid Id { get; set; }

    public required string UrlOriginal { get; set; }

    public required string UrlShorten { get; set; }

    public DateTime DateCreated { get; set; }
}