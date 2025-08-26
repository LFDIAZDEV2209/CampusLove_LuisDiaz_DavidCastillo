using DomainUser = examencsharp.src.Modules.Domain.Entities.User;
using examencsharp.src.Modules.Application.Interfaces;
using examencsharp.src.Modules.User.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace examencsharp.src.Modules.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }



    public async Task<DomainUser?> GetUserByUsernameAsync(string username)
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
    public async Task<IEnumerable<DomainUser>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userRepository.Query().ToListAsync();
            return users;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
            return Enumerable.Empty<DomainUser>();
        }
    }

    public async Task<bool> RegisterUserAsync(RegisterUserDTO userDto)
    {
        try
        {
            // Validaciones usando CultureInfo y TryParse
            if (!int.TryParse(userDto.Age.ToString(), out int age) || age < 18 || age > 100)
            {
                Console.WriteLine("La edad debe estar entre 18 y 100 años");
                return false;
            }

            if (!new[] { "Masculino", "Femenino", "Otro" }.Contains(userDto.Genre))
            {
                Console.WriteLine("El género debe ser: Masculino, Femenino u Otro");
                return false;
            }

            if (!userDto.Email.Contains("@"))
            {
                Console.WriteLine("El email debe contener @");
                return false;
            }

            var user = new DomainUser(
                id: 0, // Se auto-generará
                username: userDto.Username,
                password: userDto.Password,
                email: userDto.Email,
                name: userDto.Name,
                age: age,
                genre: userDto.Genre,
                interests: userDto.Interests,
                career: userDto.Career,
                phrase: userDto.Phrase
            );

            _userRepository.RegisterUser(user);
            await _userRepository.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar usuario: {ex.Message}, {ex.InnerException}");
            return false;
        }
    }

    public async Task<DomainUser?> LoginAsync(string username, string password)
    {
        try
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en login: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> LikeUserAsync(int currentUserId, int targetUserId)
    {
        try
        {
            var currentUser = await _userRepository.Query().FirstOrDefaultAsync(u => u.Id == currentUserId);
            if (currentUser == null || currentUser.LikesAvailable <= 0)
            {
                Console.WriteLine("No tienes likes disponibles");
                return false;
            }

            // Usar Math.Min para control de likes
            currentUser.LikesInserts = Math.Min(currentUser.LikesInserts + 1, 10);
            currentUser.LikesAvailable = Math.Max(currentUser.LikesAvailable - 1, 0);
            
            _userRepository.UpdateUser(currentUser);
            await _userRepository.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al dar like: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DislikeUserAsync(int currentUserId, int targetUserId)
    {
        try
        {
            var currentUser = await _userRepository.Query().FirstOrDefaultAsync(u => u.Id == currentUserId);
            if (currentUser == null)
            {
                return false;
            }

            currentUser.Dislikes++;
            _userRepository.UpdateUser(currentUser);
            await _userRepository.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al dar dislike: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<DomainUser>> GetPotentialMatchesAsync(int currentUserId, string currentUserGenre)
    {
        try
        {
            // Estrategia de emparejamiento usando LINQ
            var potentialMatches = await _userRepository.Query()
                .Where(u => u.Id != currentUserId)
                .Where(u => u.Genre != currentUserGenre) // Diferente género
                .Where(u => u.Age >= 18 && u.Age <= 100)
                .OrderByDescending(u => u.LikesInserts) // Priorizar usuarios populares
                .Take(10)
                .ToListAsync();

            return potentialMatches;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener matches potenciales: {ex.Message}");
            return Enumerable.Empty<DomainUser>();
        }
    }

}