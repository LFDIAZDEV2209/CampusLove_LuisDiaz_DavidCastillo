using examencsharp.src.Modules.Match.Domain.Entities;

namespace examencsharp.src.Modules.Match.Application.Interfaces;

public interface IMatchService
{
    void CreateMatch(Matches match);
}
