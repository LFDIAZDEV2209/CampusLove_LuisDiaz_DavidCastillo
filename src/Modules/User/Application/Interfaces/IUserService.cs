using examencsharp.src.Modules.Domain.Entities;

namespace examencsharp.src.Modules.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllUsersAsync();

}