using AutoMapper;
using UrlShortener.Application.DTOs.Url;
using UrlShortener.Application.Helpers;
using UrlShortener.Application.ServiceAbstractions;
using UrlShortener.DAL.Entities;
using UrlShortener.DAL.RepositoryAbstractions;

namespace UrlShortener.Application.Services;

public class ShortenUrlService : IShortenUrlService
{
    private readonly IShortenUrlRepository shortenUrlRepository;
    private readonly IMapper mapper;
    private readonly string apiUrl = "http://localhost:5000";

    public ShortenUrlService(IShortenUrlRepository shortenUrlRepository, IMapper mapper)
    {
        this.shortenUrlRepository = shortenUrlRepository;
        this.mapper = mapper;
    }
    
    public async Task<Guid> CreateShortenUrl(CreateShortenUrlDto createShortenUrlDto)
    {
        var shortenUrl = this.mapper.Map<ShortenUrl>(createShortenUrlDto);
        shortenUrl.UrlShorten = $"{apiUrl}/{UrlHelper.GenerateShortUrl()}";
        
        await this.shortenUrlRepository.AddAsync(shortenUrl);
        await this.shortenUrlRepository.SaveChangesAsync();
        
        return shortenUrl.Id;
    }

    public Task<IEnumerable<UrlSummaryDto>> GetUrlSummaries()
    {
        throw new NotImplementedException();
    }

    public Task<UrlSummaryDto> GetUrlSummaryById(Guid id)
    {
        throw new NotImplementedException();
    }
}