using examencsharp.src.Modules.Match.Domain.Entities;
using examencsharp.src.Modules.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace examencsharp.src.Shared.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<Matches> Matches { get; set; }

}