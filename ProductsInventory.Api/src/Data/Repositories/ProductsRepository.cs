using Microsoft.EntityFrameworkCore;
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

    public async Task<Product> AddAsync(Product product)
    {
        await _context.products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.products.ToListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.products.FindAsync(id);
        if (product is null)
        {
            return;
        }

        _context.products.Remove(product);
        await _context.SaveChangesAsync();
        return;
    }



    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _context.products.FindAsync(id)!;
    }

    public async Task UpdateAsync(Product product)
    {
        var existingProduct = await _context.products.FindAsync(product.Id);
        if (existingProduct is null)
        {
            throw new KeyNotFoundException("Product not found");
        }

        _context.Entry(existingProduct).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync();
    }

    // public Product Get(string id)
    // {
    //     // var product = _context.products.Find(product => product.Id == id);
    //     Product product = _context.products.Find(id);
    //     return product;
    // }

    // public List<Product> GetAll()
    // {
    //     return _context.products.ToList();
    // }

    // public void Remove(string id)
    // {
    //     // var product = _context.products.Find(product => product.Id == id);
    //     Product product = _context.products.Find(id);
    //     // products.Remove(product);
    //     _context.products.Remove(product);
    // }

    // public Product Save(Product product)
    // {
    //     _context.products.Add(product);
    //     _context.SaveChanges();
    //     return product;
    // }

    // public Product Update(Product product)
    // {
    //     _context.products.Update(product);
    //     _context.SaveChanges();
    //     return product;
    // }
}