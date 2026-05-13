namespace UrlShortener.Application.DTOs.Url;

public class UrlSummaryDto
{
    public Guid Id { get; set; }
    
    public string UrlOriginal { get; set; }
    
    public string UrlShortened { get; set; }
    
}