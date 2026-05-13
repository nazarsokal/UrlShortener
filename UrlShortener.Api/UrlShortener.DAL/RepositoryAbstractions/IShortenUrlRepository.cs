using UrlShortener.DAL.Entities;

namespace UrlShortener.DAL.RepositoryAbstractions;

public interface IShortenUrlRepository : IUrlShortenerRepository<ShortenUrl>
{
    Task<ShortenUrl?> GetByShortCodeAsync(string shortCode);
    
    Task<ShortenUrl?> GetByUrlAsync(string url);
}