using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    public Task CreateAsync(Book entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(params string[] names)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(params Guid[] Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public void Update(Book entity)
    {
        throw new NotImplementedException();
    }
}
