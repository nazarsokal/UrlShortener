using UrlShortener.DAL.Entities;

namespace UrlShortener.DAL.Repositories.RepositoryAbstractions;

public interface IUrlShortenerRepository<T> where T : class
{
    Task<ShortenUrl?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<T>> GetAllAsync();
    
    Task AddAsync(T entity);
    
    void Update(T entity);
    
    void Delete(T entity);
    
    Task SaveChangesAsync();
}