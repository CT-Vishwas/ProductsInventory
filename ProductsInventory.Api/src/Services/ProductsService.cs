using AutoMapper;
using ProductsInventory.Api.Data.DTOs;
using ProductsInventory.Api.Data.Requests;
using ProductsInventory.Api.Entities;
using ProductsInventory.Api.Repositories;

namespace ProductsInventory.Api.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;

    private readonly IMapper _mapper;


    public ProductsService(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;

    }

    public async Task<ProductDto> CreateProduct(CreateProductRequest createProduct)
    {
        Product product = _mapper.Map<Product>(createProduct);
        await _productsRepository.AddAsync(product);
        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;

    }

    public async Task<IEnumerable<ProductDto>> GetAll()
    {
        var products = await _productsRepository.GetProductsAsync();
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return productDtos;
    }

    public async Task<ProductDto> GetById(Guid id)
    {
        var product = await _productsRepository.GetByIdAsync(id);
        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }
    
        public async Task<ProductDto> UpdateProduct(Guid id, CreateProductRequest request)
    {
        var product = await _productsRepository.GetByIdAsync(id);
        if (product == null)
        {
            return null;
        }

        _mapper.Map(request, product);
        await _productsRepository.UpdateAsync(product);

        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = _productsRepository.GetByIdAsync(id);
        if(product is null)
        {
            return false;
        }
        await _productsRepository.DeleteAsync(id);
        return true;

    }
    // public Product AddProduct(Product product)
    // {
    //     return _productsRepository.Save(product);
    // }

    // public void DeleteProduct(string id)
    // {
    //     Product product = _productsRepository.Get(id);
    //     if (product == null)
    //     {
    //         // throw new ResourceNotFound();
    //         throw new Exception("Product Not Found");
    //     }
    //     _productsRepository.Remove(id);
    // }

    // public List<Product> GetAllProducts()
    // {
    //     List<Product> products = _productsRepository.GetAll();
    //     return products;
    // }

    // public Product GetProduct(string id)
    // {
    //     return _productsRepository.Get(id);
    // }

    // public Product UpdateProduct(string id, Product product)
    // {
    //     // Check if Product Exists
    //     Product dbproduct = _productsRepository.Get(id);
    //     if (dbproduct == null)
    //     {
    //         // Logic 1
    //         // throw new ResourceNotFoundException();
    //         throw new Exception("Product Not Found");
    //     }

    //     if (product.Name != "")
    //     {
    //         dbproduct.Name = product.Name;
    //     }


    //     Product updatedProduct = _productsRepository.Update(dbproduct);

    //     return updatedProduct;

    // }

}
