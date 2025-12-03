using Models;

namespace Infrastructure.Repositories.RepositoryContracts;

public interface IUrlShortenerRepository
{
    public Task<Guid> ShortenUrlAsync(ShortenUrl shortenUrlToAdd);
    
    public Task<List<ShortenUrl>> GetShortenedUrlsAsync();
    
    public Task<ShortenUrl> GetShortenedUrlAsync(Guid id, Guid userId);
}