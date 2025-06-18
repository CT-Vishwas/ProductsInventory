using ProductsInventory.Api.Entities;

namespace ProductsInventory.Api.Services
{
    public interface IProductsService
    {
        public Product GetProduct(string id);

        public Product AddProduct(Product product);
        public List<Product> GetAllProducts();

        public void DeleteProduct(string id);

        public Product UpdateProduct(string id, Product product);


    }
}