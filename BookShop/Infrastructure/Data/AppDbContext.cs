using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }


    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Basket> Baskets => Set<Basket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var book = modelBuilder.Entity<Book>();
        book.HasIndex(b => b.Title).IsUnique();
        book.Property(b => b.Title).UseCollation("SQL_Latin1_General_CP1_CI_AS");
           
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        modelBuilder.Entity<User>().Navigation(u => u.Basket).AutoInclude();
        modelBuilder.Entity<Basket>().Navigation(b => b.Books).AutoInclude();
        modelBuilder.Entity<UserProfile>().Navigation(b => b.User).AutoInclude();
        modelBuilder.Entity<Order>().Navigation(b => b.User).AutoInclude();

    }
}
