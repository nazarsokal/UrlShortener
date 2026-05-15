using UrlShortener.DAL.Entities;

namespace UrlShortener.DAL.RepositoryAbstractions;

public interface IUrlShortenerRepository<T> where T : class
{
    Task<ShortenUrl?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<T>> GetAllAsync();
    
    Task AddAsync(T entity);
    
    void Update(T entity);
    
    Task DeleteById(Guid id);
    
    Task SaveChangesAsync();
}