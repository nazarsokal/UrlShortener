using Microsoft.EntityFrameworkCore;
using UrlShortener.DAL.Entities;
using UrlShortener.DAL.Infrastructure;
using UrlShortener.DAL.RepositoryAbstractions;

namespace UrlShortener.DAL.Repositories;

public class ShortenUrlRepository : UrlShortenerRepository<ShortenUrl>, IShortenUrlRepository
{
    private readonly UrlShortenerDbContext urlDbContext;

    public ShortenUrlRepository(UrlShortenerDbContext urlDbContext) : base(urlDbContext)
    {
        this.urlDbContext = urlDbContext;
    }

    public async Task<ShortenUrl?> GetByShortCodeAsync(string shortCode)
    {
        var shortenUrl = await this.urlDbContext.ShortenUrls.FindAsync(shortCode);
        return shortenUrl;
    }

    public Task<ShortenUrl?> GetByUrlAsync(string url)
    {
        throw new NotImplementedException();
    }

    public async Task<ShortenUrl?> GetUrlById(Guid id)
    {
        var url = await this.urlDbContext.ShortenUrls.
            Include(x => x.UserCreatedBy).
            FirstOrDefaultAsync(x => x.Id == id);
        
        return url;
    }
}