using UrlShortener.Application.DTOs.Url;

namespace UrlShortener.Application.ServiceAbstractions;

public interface IShortenUrlService
{
    Task<Guid> CreateShortenUrlAsync(CreateShortenUrlDto createShortenUrlDto);
    
    Task<IEnumerable<UrlSummaryDto>> GetUrlSummariesAsync();
    
    Task<UrlDetailDto> GetUrlDetailByIdAsync(Guid id);
    
    Task DeleteUrlAsync(Guid id);
    
    Task<IEnumerable<UrlSummaryDto>> GetUrlSummariesByUserIdAsync(Guid userId);
}