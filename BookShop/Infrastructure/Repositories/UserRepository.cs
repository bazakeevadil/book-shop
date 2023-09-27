using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> CheckUserCredentials(string username, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
        if (user is null || await HashPasswordAsync(password).ConfigureAwait(false) != user.HashPassword)
            return null;
        return user;
    }

    public async Task CreateAsync(User entity)
    {
        await _context.Users.AddAsync(entity).ConfigureAwait(false);
    }

    public async Task<bool> DeleteAsync(params string[] names)
    {
        var book = await _context.Users.Where(b => names.Contains(b.Username)).ToListAsync().ConfigureAwait(false);
        if (book.Any())
        {
            _context.Users.RemoveRange(book);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(params Guid[] Id)
    {
        var book = await _context.Users.Where(b => Id.Contains(b.Id)).ToListAsync().ConfigureAwait(false);
        if (book.Any())
        {
            _context.Users.RemoveRange(book);
            return true;
        }
        return false;
    }

    public Task<List<User>> GetAllAsync()
    {
        return _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetUserById(string username)
    {
        var book = await _context.Users.FirstOrDefaultAsync(b => b.Username == username);
        return book ?? default;
    }

    public async Task<string> HashPasswordAsync(string password)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        return await Task.FromResult(hash).ConfigureAwait(false);
    }

    public void Update(User entity)
    {
        _context.Users.Update(entity);
    }
}
