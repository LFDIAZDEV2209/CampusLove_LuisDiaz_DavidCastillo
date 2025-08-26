using DomainUser = examencsharp.src.Modules.Domain.Entities.User;

namespace examencsharp.src.Modules.Match.Domain.Entities;

public class Matches
{
    public int UserId1 { get; set; }
    public int UserId2 { get; set; }


    public DomainUser User1 { get; set; } = null!;
    public DomainUser User2 { get; set; } = null!;
}