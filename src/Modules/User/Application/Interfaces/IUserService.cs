using DomainUser = examencsharp.src.Modules.Domain.Entities.User;
using examencsharp.src.Modules.User.Application.DTOs;

namespace examencsharp.src.Modules.Application.Interfaces;

public interface IUserService
{
    Task<DomainUser?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<DomainUser>> GetAllUsersAsync();
    Task<bool> RegisterUserAsync(RegisterUserDTO userDto);
    Task<DomainUser?> LoginAsync(string username, string password);
    Task<bool> LikeUserAsync(int currentUserId, int targetUserId);
    Task<bool> DislikeUserAsync(int currentUserId, int targetUserId);
    Task<IEnumerable<DomainUser>> GetPotentialMatchesAsync(int currentUserId, string currentUserGenre);
}