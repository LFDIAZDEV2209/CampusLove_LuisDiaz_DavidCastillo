using examencsharp.src.Modules.MainMenu;
using examencsharp.src.Shared.Helpers;

namespace examencsharp;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var context = DbContextFactory.Create();

            if (context == null)
            {
                Console.WriteLine("No se pudo conectar con la base de datos");
            }

            var mainMenu = new MainMenu(context);
            await mainMenu.Show();
        }
        catch
        {
            throw new Exception("Error al conectar con la base de datos");
        }
        
        
    }
}