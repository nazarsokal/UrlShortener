using System.Security.Cryptography;
using System.Text;
using Infrastructure;
using Models.DTOs.ShortenUrlDTOs;
using Services.ServiceContracts;

namespace Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly string apiUrl = "http://localhost:5130/";
    public Task<Guid> ShortenUrlAsync(CreateShortenUrlDto createShortenedUrlDto, Guid userId)
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
}