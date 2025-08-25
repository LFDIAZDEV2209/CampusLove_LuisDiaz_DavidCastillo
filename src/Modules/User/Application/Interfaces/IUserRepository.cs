using examencsharp.src.Modules.Domain.Entities;

namespace examencsharp.src.Modules.Application.Interfaces;

public interface IUserRepository
{
    void RegisterUser(User user);
    Task<IEnumerable<User>> GetAllUsersWithoutUserLoguedAsync(string username);
    IQueryable<User> Query();
    Task<User?> GetMatchesByUsernameAsync(string username);
    Task<User?> GetUserByUsernameAsync(string username);
}