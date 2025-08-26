using examencsharp.src.Shared.Context;
using examencsharp.src.Modules.Application.Interfaces;
using examencsharp.src.Modules.Application.Services;
using examencsharp.src.Modules.User.Application.DTOs;
using examencsharp.src.Modules.Infrastructure.Repositories;
using examencsharp.src.Modules.Match.Application.Services;
using examencsharp.src.Modules.Match.Application.Interfaces;
using examencsharp.src.Modules.Match.infrastructure;
using Spectre.Console;
using System.Globalization;
using DomainUser = examencsharp.src.Modules.Domain.Entities.User;

namespace examencsharp.src.Modules.MainMenu;

public class MainMenu
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;
    private readonly IMatchService _matchService;
    private DomainUser? _currentUser;

    public MainMenu(AppDbContext context)
    {
        _context = context;
        var userRepository = new UserRepository(context);
        var matchRepository = new MatchRepository(context);
        _userService = new UserService(userRepository);
        _matchService = new MatchService(matchRepository);
    }

    public async Task Show()
    {
        while (true)
        {
            Console.Clear();

            AnsiConsole.Write(
                new FigletText("CampusLove")
                .Centered()
                .Color(Color.Red)
            );

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[bold green] Bienvenido[/]")
                .AddChoices(new[]
                {
                    "Sign up",
                    "Log in"
                }));

            switch (selection)
            {
                case "Sign up":
                    await ShowSignUpMenu();
                    break;
                case "Log in":
                    await ShowLoginMenu();
                    break;
            }
        }
    }

    private async Task ShowSignUpMenu()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Registro")
            .Centered()
            .Color(Color.Blue)
        );

        var username = AnsiConsole.Ask<string>("[bold green]Username:[/]");
        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("[bold green]Password:[/]")
            .Secret()
        );
        var email = AnsiConsole.Ask<string>("[bold green]Email:[/]");
        var name = AnsiConsole.Ask<string>("[bold green]Nombre:[/]");
        var age = AnsiConsole.Ask<int>("[bold green]Edad:[/]");
        var genre = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[bold green]Género:[/]")
            .AddChoices("Masculino", "Femenino", "Otro")
        );
        var interests = AnsiConsole.Ask<string>("[bold green]Intereses:[/]");
        var career = AnsiConsole.Ask<string>("[bold green]Carrera:[/]");
        var phrase = AnsiConsole.Ask<string>("[bold green]Frase personal:[/]");

        var userDto = new RegisterUserDTO
        {
            Username = username,
            Password = password,
            Email = email,
            Name = name,
            Age = age,
            Genre = genre,
            Interests = interests,
            Career = career,
            Phrase = phrase
        };

        var success = await _userService.RegisterUserAsync(userDto);
        if (success)
        {
            AnsiConsole.Write(new Markup("[bold green]Usuario registrado exitosamente![/]"));
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        else
        {
            AnsiConsole.Write(new Markup("[bold red]Error al registrar usuario[/]"));
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    private async Task ShowLoginMenu()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Login")
            .Centered()
            .Color(Color.Green)
        );

        var username = AnsiConsole.Ask<string>("[bold green]Username:[/]");
        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("[bold green]Password:[/]")
            .Secret()
        );

        _currentUser = await _userService.LoginAsync(username, password);
        if (_currentUser != null)
        {
            AnsiConsole.Write(new Markup("[bold green]Login exitoso![/]"));
            AnsiConsole.WriteLine();
            await ShowMainUserMenu();
        }
        else
        {
            AnsiConsole.Write(new Markup("[bold red]Credenciales incorrectas[/]"));
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    private async Task ShowMainUserMenu()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText($"Bienvenido {_currentUser!.Name}")
                .Centered()
                .Color(Color.Yellow)
            );

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[bold green]Menú Principal[/]")
                .AddChoices(new[]
                {
                    "Ver perfiles",
                    "Dar like/dislike",
                    "Ver coincidencias",
                    "Ver estadísticas",
                    "Cerrar sesión"
                }));

            switch (selection)
            {
                case "Ver perfiles":
                    await ShowProfilesMenu();
                    break;
                case "Dar like/dislike":
                    await ShowLikeDislikeMenu();
                    break;
                case "Ver coincidencias":
                    await ShowMatchesMenu();
                    break;
                case "Ver estadísticas":
                    await ShowStatisticsMenu();
                    break;
                case "Cerrar sesión":
                    _currentUser = null;
                    return;
            }
        }
    }

    private async Task ShowProfilesMenu()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Perfiles")
            .Centered()
            .Color(Color.Aqua)
        );

        var potentialMatches = await _userService.GetPotentialMatchesAsync(_currentUser!.Id, _currentUser.Genre);
        
        if (!potentialMatches.Any())
        {
            AnsiConsole.Write(new Markup("[bold yellow]No hay perfiles disponibles[/]"));
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            return;
        }

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[bold green]Selecciona un perfil:[/]")
            .AddChoices(potentialMatches.Select(u => $"{u.Name} ({u.Age}) - {u.Career}").ToArray()));

        var selectedUser = potentialMatches.FirstOrDefault(u => 
            selection.StartsWith(u.Name));

        if (selectedUser != null)
        {
            await ShowUserProfile(selectedUser);
        }
    }

    private async Task ShowUserProfile(DomainUser user)
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Perfil")
            .Centered()
            .Color(Color.Fuchsia)
        );

        var table = new Table()
            .AddColumn("Campo")
            .AddColumn("Valor");

        table.AddRow("Nombre", user.Name);
        table.AddRow("Edad", user.Age.ToString());
        table.AddRow("Género", user.Genre);
        table.AddRow("Carrera", user.Career);
        table.AddRow("Intereses", user.Interests);
        table.AddRow("Frase", user.Phrase);

        AnsiConsole.Write(table);

        var action = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[bold green]¿Qué quieres hacer?[/]")
            .AddChoices("Dar like", "Dar dislike", "Volver"));

        switch (action)
        {
            case "Dar like":
                var likeSuccess = await _userService.LikeUserAsync(_currentUser!.Id, user.Id);
                if (likeSuccess)
                {
                    AnsiConsole.Write(new Markup("[bold green]Like enviado![/]"));
                    // Refrescar usuario actual para ver contadores actualizados
                    _currentUser = await _userService.GetUserByUsernameAsync(_currentUser.Username);
                }
                else
                {
                    AnsiConsole.Write(new Markup("[bold red]No se pudo enviar el like[/]"));
                }
                break;
            case "Dar dislike":
                var dislikeSuccess = await _userService.DislikeUserAsync(_currentUser!.Id, user.Id);
                if (dislikeSuccess)
                {
                    AnsiConsole.Write(new Markup("[bold green]Dislike enviado![/]"));
                    _currentUser = await _userService.GetUserByUsernameAsync(_currentUser.Username);
                }
                else
                {
                    AnsiConsole.Write(new Markup("[bold red]No se pudo enviar el dislike[/]"));
                }
                break;
        }

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task ShowLikeDislikeMenu()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Likes/Dislikes")
            .Centered()
            .Color(Color.Purple)
        );

        AnsiConsole.Write(new Markup($"[bold green]Likes disponibles: {_currentUser!.LikesAvailable}[/]"));
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Markup($"[bold blue]Likes enviados: {_currentUser.LikesInserts}[/]"));
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Markup($"[bold red]Dislikes: {_currentUser.Dislikes}[/]"));
        AnsiConsole.WriteLine();

        AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task ShowMatchesMenu()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Coincidencias")
            .Centered()
            .Color(Color.Orange1)
        );

        var matches = await _matchService.GetAllMatchesAsync();
        var userMatches = matches.Where(m => 
            m.UserId1 == _currentUser!.Id || m.UserId2 == _currentUser.Id);

        if (!userMatches.Any())
        {
            AnsiConsole.Write(new Markup("[bold yellow]No tienes coincidencias aún[/]"));
        }
        else
        {
            var table = new Table()
                .AddColumn("Usuario")
                .AddColumn("Estado");

            foreach (var match in userMatches)
            {
                var otherUserId = match.UserId1 == _currentUser.Id ? match.UserId2 : match.UserId1;
                // Buscar por ID en lugar de username
                var allUsers = await _userService.GetAllUsersAsync();
                var otherUser = allUsers.FirstOrDefault(u => u.Id == otherUserId);
                if (otherUser != null)
                {
                    table.AddRow(otherUser.Name, "Match!");
                }
            }

            AnsiConsole.Write(table);
        }

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task ShowStatisticsMenu()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Estadísticas")
            .Centered()
            .Color(Color.Teal)
        );

        // Usar LINQ para estadísticas
        var allUsers = await _userService.GetAllUsersAsync();
        var currentUser = allUsers.FirstOrDefault(u => u.Id == _currentUser!.Id);

        if (currentUser != null)
        {
            var allMatches = await _matchService.GetAllMatchesAsync();
            var userIdToMatchCount = allUsers
                .ToDictionary(u => u.Id, u => allMatches.Count(m => m.UserId1 == u.Id || m.UserId2 == u.Id));
            var stats = new Table()
                .AddColumn("Métrica")
                .AddColumn("Valor")
                .AddColumn("Promedio/Top");

            var avgAge = allUsers.Any() ? allUsers.Average(u => u.Age) : 0;
            var avgLikes = allUsers.Any() ? allUsers.Average(u => u.LikesInserts) : 0;
            var avgDislikes = allUsers.Any() ? allUsers.Average(u => u.Dislikes) : 0;

            var topLikesUser = allUsers.OrderByDescending(u => u.LikesInserts).FirstOrDefault();
            var topDislikesUser = allUsers.OrderByDescending(u => u.Dislikes).FirstOrDefault();

            var topMatchesUser = allUsers
                .OrderByDescending(u => userIdToMatchCount.ContainsKey(u.Id) ? userIdToMatchCount[u.Id] : 0)
                .FirstOrDefault();

            stats.AddRow("Edad", currentUser.Age.ToString(), avgAge.ToString("F1"));
            stats.AddRow("Likes Enviados", currentUser.LikesInserts.ToString(), avgLikes.ToString("F1"));
            stats.AddRow("Dislikes", currentUser.Dislikes.ToString(), avgDislikes.ToString("F1"));

            if (topLikesUser != null)
                stats.AddRow("Usuario con más likes", topLikesUser.Name, topLikesUser.LikesInserts.ToString());
            if (topDislikesUser != null)
                stats.AddRow("Usuario con más dislikes", topDislikesUser.Name, topDislikesUser.Dislikes.ToString());
            if (topMatchesUser != null)
                stats.AddRow(
                    "Usuario con más matches",
                    topMatchesUser.Name,
                    (userIdToMatchCount.ContainsKey(topMatchesUser.Id) ? userIdToMatchCount[topMatchesUser.Id] : 0).ToString()
                );

            AnsiConsole.Write(stats);

            // Usar CultureInfo para formateo
            var culture = new CultureInfo("es-ES");
            AnsiConsole.WriteLine();
            var popularity = avgLikes > 0 ? (currentUser.LikesInserts / avgLikes * 100) : 0;
            AnsiConsole.Write(new Markup($"[bold green]Porcentaje de popularidad: {popularity:F1}%[/]"));
        }

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }
}