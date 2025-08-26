using examencsharp.src.Modules.Match.Application.Interfaces;
using examencsharp.src.Modules.Match.Domain.Entities;
using examencsharp.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace examencsharp.src.Modules.Match.infrastructure;

public class MatchRepository : IMatchRepository
{
    private readonly AppDbContext _context;

    public MatchRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Matches> Query()
    {
        return _context.Set<Matches>().AsNoTracking();
    }

    public Task<IEnumerable<Matches>> GetAllMatchesAsync()
    {
        return _context.Set<Matches>()
            .AsNoTracking()
            .ToListAsync()
            .ContinueWith(t => (IEnumerable<Matches>)t.Result);
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}