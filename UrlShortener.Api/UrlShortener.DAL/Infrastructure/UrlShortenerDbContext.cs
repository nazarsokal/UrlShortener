using Microsoft.EntityFrameworkCore;
using UrlShortener.DAL.Entities;

namespace UrlShortener.DAL.Infrastructure;

public class UrlShortenerDbContext : DbContext
{
    public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
        : base(options)
    {
        
    }
    
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<ShortenUrl> ShortenUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}