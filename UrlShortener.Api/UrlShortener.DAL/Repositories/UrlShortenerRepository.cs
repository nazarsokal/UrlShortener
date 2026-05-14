using Microsoft.EntityFrameworkCore;
using UrlShortener.DAL.Entities;
using UrlShortener.DAL.Infrastructure;
using UrlShortener.DAL.RepositoryAbstractions;

namespace UrlShortener.DAL.Repositories;

public class UrlShortenerRepository<T> : IUrlShortenerRepository<T> where T : class
{
    private readonly UrlShortenerDbContext urlDbContext;

    public UrlShortenerRepository(UrlShortenerDbContext urlDbContext)
    {
        this.urlDbContext = urlDbContext;
    }
    
    public async Task<ShortenUrl?> GetByIdAsync(Guid id)
    {
        return await this.urlDbContext.ShortenUrls.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await this.urlDbContext.Set<T>().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await this.urlDbContext.AddAsync(entity);
    }

    public void Update(T entity)
    {
        var updatedResult = this.urlDbContext.Update(entity).Entity;
    }

    public void DeleteById(Guid id)
    {
        this.urlDbContext.Remove(id);
    }

    public async Task SaveChangesAsync()
    {
        await this.urlDbContext.SaveChangesAsync();
    }
}