using Infrastructure.Repositories.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure.Repositories;

public class UrlShortenerRepository : IUrlShortenerRepository
{
    private readonly UrlShortenerDbContext _dbContext;

    public UrlShortenerRepository(UrlShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<Guid> ShortenUrlAsync(ShortenUrl shortenUrlToAdd)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ShortenUrl>> GetShortenedUrlsAsync()
    {
        return await _dbContext.ShortenedUrls.ToListAsync();
    }

    public async Task<ShortenUrl> GetShortenedUrlAsync(Guid id, Guid userId)
    {
        return (await _dbContext.ShortenedUrls.FirstOrDefaultAsync(s => s.Id == id && s.UserIdCreatedBy == userId))!;
    }
}