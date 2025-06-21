using ProductsInventory.Api.Data.Entities;

namespace ProductsInventory.Api.Data.Repositories;

public interface IAuthRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<bool> IsUserExistsAsync(string username);
    // Task<User> LoginAsync(User user);
    Task<User> RegisterAsync(User user);
    Task<User> GetById(Guid id);
}