using AutoMapper;
using FluentValidation;
using UrlShortener.Application.DTOs.Url;
using UrlShortener.Application.Helpers;
using UrlShortener.Application.ServiceAbstractions;
using UrlShortener.DAL.Entities;
using UrlShortener.DAL.RepositoryAbstractions;

namespace UrlShortener.Application.Services;

public class ShortenUrlService : IShortenUrlService
{
    private readonly IShortenUrlRepository shortenUrlRepository;
    private IValidator<ShortenUrl> validator;
    private readonly IMapper mapper;
    private readonly string apiUrl = "http://localhost:5017";

    public ShortenUrlService(IShortenUrlRepository shortenUrlRepository, IMapper mapper, IValidator<ShortenUrl> validator)
    {
        this.shortenUrlRepository = shortenUrlRepository;
        this.mapper = mapper;
        this.validator = validator;
    }
    
    public async Task<Guid> CreateShortenUrlAsync(CreateShortenUrlDto createShortenUrlDto)
    {
        var shortenUrl = this.mapper.Map<ShortenUrl>(createShortenUrlDto);
        shortenUrl.UrlShorten = $"{apiUrl}/{UrlHelper.GenerateShortUrl()}";
        
        await this.validator.ValidateAndThrowAsync(shortenUrl);
        
        await this.shortenUrlRepository.AddAsync(shortenUrl);
        await this.shortenUrlRepository.SaveChangesAsync();
        
        return shortenUrl.Id;
    }

    public async Task<IEnumerable<UrlSummaryDto>> GetUrlSummariesAsync()
    {
        var urls = await this.shortenUrlRepository.GetAllAsync();
        var mappedDto = this.mapper.Map<IEnumerable<UrlSummaryDto>>(urls);
        
        return mappedDto;
    }
    
    public async Task<UrlDetailDto> GetUrlDetailByIdAsync(Guid id)
    {
        var url = await this.shortenUrlRepository.GetUrlById(id);
        var mappedDto = this.mapper.Map<UrlDetailDto>(url);
        
        return mappedDto;
    }

    public async Task DeleteUrlAsync(Guid id)
    {
        await this.shortenUrlRepository.DeleteById(id);
        await this.shortenUrlRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<UrlSummaryDto>> GetUrlSummariesByUserIdAsync(Guid userId)
    {
        var urls = await this.shortenUrlRepository.GetUrlsByUserId(userId);
        var mappedDto = this.mapper.Map<IEnumerable<UrlSummaryDto>>(urls);
        
        return mappedDto;
    }

    public async Task<string> GetOriginalUrlByShortCodeAsync(string shortCode)
    {
        var foundUrl = await this.shortenUrlRepository.GetByShortCodeAsync($"{apiUrl}/{shortCode}");

        if (foundUrl != null) 
            return foundUrl.UrlOriginal;
        
        return string.Empty;
    }
}