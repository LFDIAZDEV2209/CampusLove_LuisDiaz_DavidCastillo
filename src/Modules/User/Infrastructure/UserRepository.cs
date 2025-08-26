using examencsharp.src.Modules.Application.Interfaces;
using DomainUser = examencsharp.src.Modules.Domain.Entities.User;
using examencsharp.src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace examencsharp.src.Modules.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<DomainUser> Query()
    {
        return _context.User.AsNoTracking();
    }

    public Task<IEnumerable<DomainUser>> GetAllUsersWithoutUserLoguedAsync(string username)
    {
        return _context.User
            .AsNoTracking()
            .Where(u => u.Username != username)
            .ToListAsync()
            .ContinueWith(t => (IEnumerable<DomainUser>)t.Result);
    }

    public Task<DomainUser?> GetMatchesByUsernameAsync(string username)
    {
        return _context.User
            .AsNoTracking()
            .Include(u => u.MatchesAsUser1)
            .Include(u => u.MatchesAsUser2)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public Task<DomainUser?> GetUserByUsernameAsync(string username)
    {
        return _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public void RegisterUser(DomainUser user)
    {
        _context.User.Add(user);
    }

    public void UpdateUser(DomainUser user)
    {
        var local = _context.User.Local.FirstOrDefault(e => e.Id == user.Id);
        if (local != null)
        {
            _context.Entry(local).State = EntityState.Detached;
        }
        _context.User.Attach(user);
        _context.Entry(user).State = EntityState.Modified;
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}
