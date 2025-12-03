namespace Models.DTOs.ShortenUrlDTOs;

public class GetShortenUrlSummaryDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string ShortenedLink { get; set; }
}