using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<User?> CheckUserCredentials(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(User entity)
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

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByName(string username)
    {
        throw new NotImplementedException();
    }

    public Task<string> HashPasswordAsync(string password)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }
}
