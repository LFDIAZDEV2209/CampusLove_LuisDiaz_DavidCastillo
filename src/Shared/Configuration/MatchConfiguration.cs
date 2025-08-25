using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using examencsharp.src.Modules.Match.Domain.Entities;

namespace examencsharp.src.Shared.Configuration;

public class Matchconfiguration
{
    public void Configure(EntityTypeBuilder<Matches> builder)
    {
        builder.ToTable("matches");

        builder.HasKey(m => new { m.UserId1, m.UserId2 });

        builder.Property(m => m.UserId1)
               .HasColumnName("userId1")
               .IsRequired();

        builder.Property(m => m.UserId2)
               .HasColumnName("userId2")
               .IsRequired();

        builder.HasOne(m => m.User1)
               .WithMany(e => e.MatchesAsUser1)
               .HasForeignKey(m => m.UserId1);

        builder.HasOne(m => m.User2)
               .WithMany(e => e.MatchesAsUser2)
               .HasForeignKey(m => m.UserId2);
    }
}