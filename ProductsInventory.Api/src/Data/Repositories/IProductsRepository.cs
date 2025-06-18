using ProductsInventory.Api.Entities;

namespace ProductsInventory.Api.Repositories;

public interface IProductsRepository
{
    public Product Save(Product product);
    public List<Product> GetAll();
    public Product Get(string id);
    public Product Update(Product product);
    public void Remove(string id);
}