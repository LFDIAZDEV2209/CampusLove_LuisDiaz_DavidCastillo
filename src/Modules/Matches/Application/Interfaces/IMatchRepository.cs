using examencsharp.src.Modules.Match.Domain.Entities;

namespace examencsharp.src.Modules.Match.Application.Interfaces;

public interface IMatchRepository
{
    IQueryable<Matches> Query();
    Task<IEnumerable<Matches>> GetAllMatchesAsync();
    Task<int> SaveAsync();
}