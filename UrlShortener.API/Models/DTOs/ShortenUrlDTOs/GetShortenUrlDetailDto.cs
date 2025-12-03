namespace Models.DTOs.ShortenUrlDTOs;

public class GetShortenUrlDetailDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string OriginalLink { get; set; }
    
    public string ShortenedLink { get; set; }
    
    public DateTime CreatedAt { get; set; }
}