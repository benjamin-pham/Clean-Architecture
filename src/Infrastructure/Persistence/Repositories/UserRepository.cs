using Application.Libraries;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.BaseRepositories;

namespace Persistence.Repositories;
public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    private readonly IHash _hash;
    public UserRepository(ApplicationDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
    {
        _hash = serviceProvider.GetService<IHash>();
    }

    public string HashPassword(string password)
    {
        return _hash.HashSHA1(password);
    }

    public async Task CheckInfoIsUniqueAsync(User userInfo)
    {
        var result = await _dbContext.Set<User>().AsQueryable().Where(user => (user.Username == userInfo.Username || user.Email == userInfo.Email) && user.Id != userInfo.Id).AnyAsync();
        if (result)
        {
            Result.Failure("email or username already exists in the system");
        }
    }

    public Task<User> LoginVerificationAsync(string username, string passwordHash)
    {
        return _dbContext.Set<User>().AsQueryable().Where(user => user.Username == username && user.PasswordHash == passwordHash).SingleOrDefaultAsync();
    }
}
