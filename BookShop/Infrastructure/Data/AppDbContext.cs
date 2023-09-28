using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }


    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Basket> Baskets => Set<Basket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var book = modelBuilder.Entity<Book>();
        book.HasIndex(b => b.Title).IsUnique();
        book.Property(b => b.Title).UseCollation("SQL_Latin1_General_CP1_CI_AS");
        book.Property(b => b.Price)
            .HasColumnType("decimal(10, 2)");

        var user = modelBuilder.Entity<User>();
        user.HasIndex(u => u.Username).IsUnique();
        user.Property(u => u.Username).UseCollation("SQL_Latin1_General_CP1_CI_AS");
        
        modelBuilder.Entity<User>().Navigation(u => u.Basket).AutoInclude();
        modelBuilder.Entity<Basket>().Navigation(b => b.Books).AutoInclude();
        modelBuilder.Entity<Order>().Navigation(o => o.User).AutoInclude();
        modelBuilder.Entity<Order>().Property(b => b.TotalPrice)
            .HasColumnType("decimal(10, 2)");

    }
}
