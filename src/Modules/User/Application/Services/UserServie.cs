using examencsharp.src.Modules.Domain.Entities;
using examencsharp.src.Modules.Application.Interfaces;

namespace examencsharp.src.Modules.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }



    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        try
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                Console.WriteLine("usuario no encontrado");
                return null;
            }
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener el usuario: {ex.Message}");
            return null;
        }
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userRepository.GetAllUsersWithoutUserLoguedAsync("username");
            return users;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
            return Enumerable.Empty<User>();
        }
    }

}