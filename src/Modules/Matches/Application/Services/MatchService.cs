using examencsharp.src.Modules.Match.Application.Interfaces;
using examencsharp.src.Modules.Match.Domain.Entities;
using examencsharp.src.Modules.Match.infrastructure;

namespace examencsharp.src.Modules.Match.Application.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;

    public MatchService(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public void CreateMatch(Matches match)
    {
        try
        {
            // Validar que no exista ya el match
            var existingMatch = _matchRepository.Query()
                .FirstOrDefault(m => 
                    (m.UserId1 == match.UserId1 && m.UserId2 == match.UserId2) ||
                    (m.UserId1 == match.UserId2 && m.UserId2 == match.UserId1));

            if (existingMatch == null)
            {
                // Aquí se implementaría la lógica para crear el match
                // Por ahora solo validamos que no exista
                Console.WriteLine("Match creado exitosamente");
            }
            else
            {
                Console.WriteLine("El match ya existe");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear el match: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Matches>> GetAllMatchesAsync()
    {
        try
        {
            return await _matchRepository.GetAllMatchesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener los matches: {ex.Message}");
            return Enumerable.Empty<Matches>();
        }
    }
}
