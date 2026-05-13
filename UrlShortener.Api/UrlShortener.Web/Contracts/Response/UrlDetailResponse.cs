namespace UrlShortener.Web.Contracts.Response;

public class UrlDetailResponse
{
    public Guid Id { get; set; }
    
    public required string UrlOriginal { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public string? Description { get; set; }
    
    public required string CreatedByUser { get; set; }
}

