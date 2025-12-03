using Infrastructure;
using Models.DTOs.ShortenUrlDTOs;
using Services.ServiceContracts;

namespace Services;

public class UrlShortenerService : IUrlShortenerService
{
    public Task<Guid> ShortenUrlAsync(CreateShortenUrlDto createShortenedUrlDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetShortenUrlSummaryDto>> GetShortenedUrlsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetShortenUrlDetailDto> GetShortenedUrlAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }
}