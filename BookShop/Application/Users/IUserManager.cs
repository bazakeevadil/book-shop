using Domain.Entities;

namespace Application.Users;

public interface IUserManager
{
    Task CreateAsync(User user,string password);
    Task<bool> ExistsAsync(string username);
    Task<User?> GetByUsernameAsync(string username);
}
