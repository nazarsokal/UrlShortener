using UrlShortener.Application.DTOs.Url;

namespace UrlShortener.Application.ServiceAbstractions;

public interface IShortenUrlService
{
    Task<Guid> CreateShortenUrl(CreateShortenUrlDto createShortenUrlDto);
    
    Task<IEnumerable<UrlSummaryDto>> GetUrlSummaries();
    
    Task<UrlSummaryDto> GetUrlSummaryById(Guid id);
    
}