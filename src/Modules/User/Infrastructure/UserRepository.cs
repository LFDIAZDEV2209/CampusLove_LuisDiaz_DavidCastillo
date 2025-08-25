using examencsharp.src.Modules.Application.Interfaces;
using examencsharp.src.Modules.Domain.Entities;
using examencsharp.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace examencsharp.src.Modules.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<User> Query()
    {
        return _context.User.AsNoTracking();
    }

    public Task<IEnumerable<User>> GetAllUsersWithoutUserLoguedAsync(string username)
    {
        return _context.User
            .AsNoTracking()
            .Where(u => u.Username != username)
            .ToListAsync()
            .ContinueWith(t => (IEnumerable<User>)t.Result);
    }

    public Task<User?> GetMatchesByUsernameAsync(string username)
    {
        return
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public void RegisterUser(User user)
    {
        throw new NotImplementedException();
    }
}
