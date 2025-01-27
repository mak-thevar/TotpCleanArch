using Microsoft.EntityFrameworkCore;
using TotpCleanArch.Domain.Entities;

namespace TotpCleanArch.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) 
    {
        //ensure db is created
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<TotpSettings> TotpSettings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .HasOne(u => u.TotpSettings)
            .WithOne(ts => ts.User)
            .HasForeignKey<TotpSettings>(ts => ts.UserId);

        modelBuilder.Entity<TotpSettings>()
            .HasKey(ts => ts.Id);
    }
}
