using ProductsInventory.Api.Entities;
using ProductsInventory.Api.Repositories;

namespace ProductsInventory.Api.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;


    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
        
    }

    public Product AddProduct(Product product)
    {
        return _productsRepository.Save(product);
    }

    public void DeleteProduct(string id)
    {
        Product product = _productsRepository.Get(id);
        if (product == null)
        {
            // throw new ResourceNotFound();
            throw new Exception("Product Not Found");
        }
        _productsRepository.Remove(id);
    }

    public List<Product> GetAllProducts()
    {
        List<Product> products = _productsRepository.GetAll();
        return products;
    }

    public Product GetProduct(string id)
    {
        return _productsRepository.Get(id);
    }

    public Product UpdateProduct(string id, Product product)
    {
        // Check if Product Exists
        Product dbproduct = _productsRepository.Get(id);
        if (dbproduct == null)
        {
            // Logic 1
            // throw new ResourceNotFoundException();
            throw new Exception("Product Not Found");
        }

        if (product.Name != "")
        {
            dbproduct.Name = product.Name;
        }


        Product updatedProduct = _productsRepository.Update(dbproduct);

        return updatedProduct;

    }

}
