using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs.ShortenUrlDTOs;
using Services.ServiceContracts;

namespace Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly UrlShortenerDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly string apiUrl = "http://localhost:5130/";

    public UrlShortenerService(UrlShortenerDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Guid> SaveShortenUrlAsync(CreateShortenUrlDto createShortenedUrlDto, Guid userId)
    {
        var shortenedUrl = _mapper.Map<ShortenUrl>(createShortenedUrlDto);
        shortenedUrl.UserIdCreatedBy = userId;
        
        if (createShortenedUrlDto.ShortenUrl == null)
        {
            var shortenedLink = ShortenUrl(createShortenedUrlDto.OriginalLink);
            shortenedUrl.ShortenedLink = shortenedLink;
        }
        
        if(await ShortenUrlExists(shortenedUrl.ShortenedLink))
            return Guid.Empty;
        
        var result = await _dbContext.ShortenedUrls.AddAsync(shortenedUrl);
        var rows = await _dbContext.SaveChangesAsync();
        
        if(rows > 0)
            return shortenedUrl.Id;
        
        return Guid.Empty;
    }

    public async Task<string> ShortenUrlAsync(string urlToShorten)
    {
        var shortenedUrl = ShortenUrl(urlToShorten);
        bool ifExists = await _dbContext.ShortenedUrls.AnyAsync(s => s.ShortenedLink == shortenedUrl);
        
        if(ifExists)
            return String.Empty;
        
        return shortenedUrl;
    }

    public async Task<List<GetShortenUrlSummaryDto>> GetShortenedUrlsAsync()
    {
        var shortenUrls = await _dbContext.ShortenedUrls.ToListAsync();
        
        return _mapper.Map<List<GetShortenUrlSummaryDto>>(shortenUrls);
    }

    public async Task<GetShortenUrlDetailDto> GetShortenedUrlAsync(Guid id)
    {
        var shortenUrl = await _dbContext.ShortenedUrls
            .FirstOrDefaultAsync(s => s.Id == id);
        
        var shortenUrlDto = _mapper.Map<GetShortenUrlDetailDto>(shortenUrl);
        return shortenUrlDto;
    }

    public async Task<string> GetShortenedUrlByShortenedLinkAsync(string shortenedLink)
    {
        var result = await _dbContext.ShortenedUrls.FirstOrDefaultAsync(s => s.ShortenedLink.Contains(shortenedLink));
        return result!.OriginalLink;
    }

    public async Task<bool> DeleteShortenedUrlAsync(Guid id)
    {
        var result = await _dbContext.ShortenedUrls.FirstOrDefaultAsync(s => s.Id == id);
        if (result == null)
            return false;
        _dbContext.ShortenedUrls.Remove(result);
        var rows = await _dbContext.SaveChangesAsync();
        return rows > 0;
    }

    private string ShortenUrl(string url, int length = 8)
    {
        var bytes = new byte[length];
        RandomNumberGenerator.Fill(bytes);
        
        var base64 = Convert.ToBase64String(bytes).Replace("+", "").Replace("/", "").Replace("=", "");
        
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(apiUrl);
        stringBuilder.Append(base64);
        
        return stringBuilder.ToString();
    }
    
    private async Task<bool> ShortenUrlExists(string url) => 
        await _dbContext.ShortenedUrls.AnyAsync(s => s.ShortenedLink == url);
}