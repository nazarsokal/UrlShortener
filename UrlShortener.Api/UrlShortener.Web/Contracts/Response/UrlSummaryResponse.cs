namespace UrlShortener.Web.Contracts.Response;

public class UrlSummaryResponse
{
    public Guid Id { get; set; }
    
    public string UrlOriginal { get; set; }
    
    public string UrlShortened { get; set; }
}

