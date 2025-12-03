using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure;

public class UrlShortenerDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public virtual DbSet<ShortenUrl> ShortenedUrls { get; set; }
    
    public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ShortenUrl>().ToTable("ShortenUrls");

        builder.Entity<ShortenUrl>()
            .HasOne(s => s.ApplicationUserCreated)
            .WithMany(u => u.ShortenedUrls)
            .HasForeignKey(s => s.UserIdCreatedBy);
    }
}