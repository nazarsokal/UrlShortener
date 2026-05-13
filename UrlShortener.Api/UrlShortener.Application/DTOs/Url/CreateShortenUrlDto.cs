namespace UrlShortener.Application.DTOs.Url;

public class CreateShortenUrlDto
{
    public Guid Id { get; set; }
    
    public required string UrlOriginal { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public Guid UserId { get; set; }
}