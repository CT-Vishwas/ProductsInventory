using Microsoft.AspNetCore.Mvc;
using ProductsInventory.Api.Entities;
using ProductsInventory.Api.Services;

namespace ProductsInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }


    [HttpPost]
    public ActionResult CreateProduct([FromBody] Product product)
    {
        Product newProduct = _productsService.AddProduct(product);
        return Ok(newProduct);
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        IEnumerable<Product> products = _productsService.GetAllProducts();
        return Ok(products);
    }

    // GET: http://localhost/Products/1
    [HttpGet("{id}")]
    public ActionResult GetProduct(string id)
    {
        Product product = _productsService.GetProduct(id);
        return Ok(product);
    }


    [HttpPut("{id}")]
    public ActionResult UpdateProduct([FromBody] Product product, string id)
    {
        Product product1 = _productsService.UpdateProduct(id, product);
        return Ok(product1);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(string id)
    {
        _productsService.DeleteProduct(id);
        return NoContent();
    }
}