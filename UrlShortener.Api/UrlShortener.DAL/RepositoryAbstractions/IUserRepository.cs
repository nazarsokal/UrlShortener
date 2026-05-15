using UrlShortener.DAL.Entities;

namespace UrlShortener.DAL.RepositoryAbstractions;

public interface IUserRepository : IUrlShortenerRepository<User>
{
    public Task<User?> GetUserByUsernameAsync(string username);
}