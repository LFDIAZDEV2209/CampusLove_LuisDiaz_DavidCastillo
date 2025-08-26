namespace examencsharp.src.Modules.User.Application.DTOs;

public class RegisterUserDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Genre { get; set; } = string.Empty;
    public string Interests { get; set; } = string.Empty;
    public string Career { get; set; } = string.Empty;
    public string Phrase { get; set; } = string.Empty;
}
