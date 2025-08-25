namespace examencsharp.src.Modules.Match.Domain.Entities;
public class Matches
{
    public string User1 { get; set; } = string.Empty;
    public string User2 { get; set; } = string.Empty;

    public Matches(int id, string user1, string user2)
    {
        User1 = user1;
        User2 = user2;
    }
}