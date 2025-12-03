using Models.DTOs.ShortenUrlDTOs;

namespace Services.ServiceContracts;

public interface IUrlShortenerService
{
    public Task<Guid> ShortenUrlAsync(CreateShortenUrlDto createShortenedUrlDto, Guid userId);
    
    public Task<List<GetShortenUrlSummaryDto>> GetShortenedUrlsAsync();
    
    public Task<GetShortenUrlDetailDto> GetShortenedUrlAsync(Guid id, Guid userId);
}