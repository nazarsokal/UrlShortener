namespace Models.DTOs.ShortenUrlDTOs;

public class CreateShortenUrlDto
{
    public string Name { get; set; }
    
    public string OriginalLink { get; set; }

    public string? ShortenUrl { get; set; }
}