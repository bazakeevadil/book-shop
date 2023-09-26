using Application.Users;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

internal class UserManager : IUserManager
{
    private readonly AppDbContext _context;

    public UserManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user, string password)
    {
        var oldUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
        if (oldUser != null)
            throw new Exception();

        _context.Users.Add(user);
    }

    public async Task<bool> ExistsAsync(string username)
    {
        var user = await _context.FindAsync<User>(username);

        return user != null;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var result = await _context.FindAsync<User>(username);

        if (result == null)
            return null;

        var user = new User
        {
            Username = result.Username,
            Password = result.Password,
            UserRole = result.UserRole,
        };
        return user;
    }
}
