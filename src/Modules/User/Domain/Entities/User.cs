

namespace examencsharp.src.Modules.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Genre { get; set; } = string.Empty;
    public string Interests { get; set; } = string.Empty;
    public string Career { get; set; } = string.Empty;

    public string Phrase { get; set; } = string.Empty;

    public int LikesInserts { get; set; }
    public int LikesAvailable { get; set; }
    public int Dislikes { get; set; }

    public User(int id, string username, string password, string email, string name, int age, string genre, string interests, string career, string phrase)
    {
        Id = id;
        Username = username;
        Password = password;
        Email = email;
        Name = name;
        Age = age;
        Genre = genre;
        Interests = interests;
        Career = career;
        Phrase = phrase;
        LikesInserts = 0;
        LikesAvailable = 5; // Inicialmente tiene 5 likes disponibles
        Dislikes = 0;
    }
}
