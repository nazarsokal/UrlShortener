using Models.DTOs.ShortenUrlDTOs;

namespace Services.ServiceContracts;

public interface IUrlShortenerService
{
    public Task<Guid> ShortenUrlAndSaveAsync(CreateShortenUrlDto createShortenedUrlDto, Guid userId);
    public Task<string> ShortenUrlAsync(string urlToShorten);
    
    public Task<List<GetShortenUrlSummaryDto>> GetShortenedUrlsAsync();
    
    public Task<GetShortenUrlDetailDto> GetShortenedUrlAsync(Guid id);
}