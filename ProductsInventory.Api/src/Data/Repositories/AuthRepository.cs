using Microsoft.EntityFrameworkCore;
using ProductsInventory.Api.Data.Entities;

namespace ProductsInventory.Api.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    // private List<Product> products;
    public ApplicationDbContext _context;
    public AuthRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public Task<User> GetById(Guid id)
    {
        var user = _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public Task<User> GetUserByUsernameAsync(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        return Task.FromResult(user);
    }

    public Task<bool> IsUserExistsAsync(string username)
    {
        var userExists = _context.Users.Any(u => u.Username == username);
        return Task.FromResult(userExists);
    }


    public async Task<User> RegisterAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}