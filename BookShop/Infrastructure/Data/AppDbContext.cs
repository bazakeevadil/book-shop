using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }


    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Author => Set<Author>();
    public DbSet<Basket> Baskets => Set<Basket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Title)
            .IsUnique();
        modelBuilder.Entity<Author>()
            .HasIndex(a => a.Fullname)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        modelBuilder.Entity<User>().Navigation(u => u.Basket).AutoInclude();
        modelBuilder.Entity<Basket>().Navigation(b => b.Books).AutoInclude();
        modelBuilder.Entity<Author>().Navigation(b => b.Books).AutoInclude();
    }
}
