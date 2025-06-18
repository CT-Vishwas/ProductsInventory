using ProductsInventory.Api.Data;
using ProductsInventory.Api.Entities;

namespace ProductsInventory.Api.Repositories;

public class ProductsRepository : IProductsRepository
{
    // private List<Product> products;
    public ApplicationDbContext _context;
    public ProductsRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public Product Get(string id)
    {
        // var product = _context.products.Find(product => product.Id == id);
        Product product = _context.products.Find(id);
        return product;
    }

    public List<Product> GetAll()
    {
        return _context.products.ToList();
    }

    public void Remove(string id)
    {
        // var product = _context.products.Find(product => product.Id == id);
        Product product = _context.products.Find(id);
        // products.Remove(product);
        _context.products.Remove(product);
    }

    public Product Save(Product product)
    {
        _context.products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product Update(Product product)
    {
        _context.products.Update(product);
        _context.SaveChanges();
        return product;
    }
}