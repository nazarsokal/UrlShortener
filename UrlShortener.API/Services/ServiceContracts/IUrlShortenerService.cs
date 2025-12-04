using Models.DTOs.ShortenUrlDTOs;

namespace Services.ServiceContracts;

public interface IUrlShortenerService
{
    public Task<Guid> SaveShortenUrlAsync(CreateShortenUrlDto createShortenedUrlDto, Guid userId);
    public Task<string> ShortenUrlAsync(string urlToShorten);
    
    public Task<List<GetShortenUrlSummaryDto>> GetShortenedUrlsAsync();
    
    public Task<GetShortenUrlDetailDto> GetShortenedUrlAsync(Guid id);
    
    public Task<string> GetShortenedUrlByShortenedLinkAsync(string shortenedLink);
    
    public Task<bool> DeleteShortenedUrlAsync(Guid id);
}