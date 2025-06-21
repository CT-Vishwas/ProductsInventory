using ProductsInventory.Api.Data.DTOs;
using ProductsInventory.Api.Data.Requests;
using ProductsInventory.Api.Entities;

namespace ProductsInventory.Api.Services
{
    public interface IProductsService
    {
        // public Product GetProduct(string id);

        // public Product AddProduct(Product product);
        // public List<Product> GetAllProducts();

        // public void DeleteProduct(string id);

        // public Product UpdateProduct(string id, Product product);

        Task<ProductDto> CreateProduct(CreateProductRequest createProduct);
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> GetById(Guid id);

        Task<ProductDto> UpdateProduct(Guid id, CreateProductRequest request);
        Task<bool> DeleteProductAsync(Guid id);

    }
}