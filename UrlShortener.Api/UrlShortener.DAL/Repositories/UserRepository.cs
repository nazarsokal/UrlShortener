using Microsoft.EntityFrameworkCore;
using UrlShortener.DAL.Entities;
using UrlShortener.DAL.Infrastructure;
using UrlShortener.DAL.RepositoryAbstractions;

namespace UrlShortener.DAL.Repositories;

public class UserRepository : UrlShortenerRepository<User>, IUserRepository
{
    private readonly UrlShortenerDbContext urlDbContext;
    public UserRepository(UrlShortenerDbContext urlDbContext) : base(urlDbContext)
    {
        this.urlDbContext = urlDbContext;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var userFound = await urlDbContext.Users.Include(x => x.ShortenUrls).FirstOrDefaultAsync(x => x.Username == username);
        return userFound;
    }
}