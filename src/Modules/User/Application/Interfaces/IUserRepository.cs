using DomainUser = examencsharp.src.Modules.Domain.Entities.User;

namespace examencsharp.src.Modules.Application.Interfaces;

public interface IUserRepository
{
    void RegisterUser(DomainUser user);
    void UpdateUser(DomainUser user);
    Task<IEnumerable<DomainUser>> GetAllUsersWithoutUserLoguedAsync(string username);
    IQueryable<DomainUser> Query();
    Task<DomainUser?> GetMatchesByUsernameAsync(string username);
    Task<DomainUser?> GetUserByUsernameAsync(string username);
    Task<int> SaveAsync();
}