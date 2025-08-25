using examencsharp.src.Shared.Context;
using Spectre.Console;

namespace examencsharp.src.Modules.MainMenu;

public class MainMenu
{
    private readonly AppDbContext _context;

    public MainMenu(AppDbContext context)
    {
        _context = context;
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
                    Console.Clear();
                    AnsiConsole.Write("Funcionalidad en desarrollo");
                    break;
                case "Log in":
                    Console.Clear();
                    AnsiConsole.Write("Funcionalidad en desarrollo");
                    break;
            }
        }
        
    }
}