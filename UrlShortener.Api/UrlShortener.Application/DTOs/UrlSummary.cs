namespace UrlShortener.Application.DTOs;

public class UrlSummary
{
    public Guid Id { get; set; }
    
    public string UrlOriginal { get; set; }
    
    public string UrlShortened { get; set; }
    
}