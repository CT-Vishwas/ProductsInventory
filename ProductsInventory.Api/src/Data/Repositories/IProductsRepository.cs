using ProductsInventory.Api.Entities;

namespace ProductsInventory.Api.Repositories;

public interface IProductsRepository
{
    // public Product Save(Product product);
    // public List<Product> GetAll();
    // public Product Get(string id);
    // public Product Update(Product product);
    // public void Remove(string id);

    Task<Product> AddAsync(Product product);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task DeleteAsync(Guid id);
    Task<Product> GetByIdAsync(Guid id);
    Task UpdateAsync(Product product);
}