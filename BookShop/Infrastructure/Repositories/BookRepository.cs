using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Book entity)
    {
        await _context.Books.AddAsync(entity).ConfigureAwait(false);
    }

    public async Task<bool> DeleteAsync(params string[] names)
    {
        var book = await _context.Books.Where(b => names.Contains(b.Title)).ToListAsync().ConfigureAwait(false);
        if (book.Any())
        {
            _context.Books.RemoveRange(book);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(params Guid[] Id)
    {
        var book = await _context.Books.Where(b => Id.Contains(b.Id)).ToListAsync().ConfigureAwait(false);
        if (book.Any())
        {
            _context.Books.RemoveRange(book);
            return true;
        }
        return false;
    }

    public Task<List<Book>> GetAllAsync()
    {
        return _context.Books.AsNoTracking().ToListAsync();
    }

    public async Task<Book?> GetByTitle(string title)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Title == title);
        return book ?? default;
    }

    public void Update(Book entity)
    {
        _context.Books.Update(entity);
    }
}
