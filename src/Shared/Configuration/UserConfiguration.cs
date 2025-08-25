using examencsharp.src.Modules.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace examencsharp.src.Shared.Configuration;

public class UserConfiguration
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
      
        builder.ToTable("user");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Age)
            .HasColumnName("age")
            .IsRequired();

        builder.Property(e =>
e.Genre)
                .HasColumnName("genre")
                .IsRequired()
                .HasMaxLength(50);  
        builder.Property(e => e.Interests)
            .HasColumnName("interests")
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(e => e.Career)
            .HasColumnName("career")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Phrase)
            .HasColumnName("phrase")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.LikesInserts)
            .HasColumnName("likesInserts")
            .IsRequired();  

        builder.Property(e => e.LikesAvailable)
            .HasColumnName("likesAvailable")
            .IsRequired();

        builder.Property(e => e.Dislikes)
            .HasColumnName("dislikes")
            .IsRequired();

        builder.Property(e => e.Username)
            .HasColumnName("username")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Password)
            .HasColumnName("password")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(100);
        
    }
}
