using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsInventory.Api.Data.DTOs;
using ProductsInventory.Api.Data.Requests;
using ProductsInventory.Api.Data.Responses;
using ProductsInventory.Api.Entities;
using ProductsInventory.Api.Services;

namespace ProductsInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductsService _productService;

    public ProductsController(IProductsService productsService)
    {
        _productService = productsService;
    }


    // [HttpPost]
    // public ActionResult CreateProduct([FromBody] Product product)
    // {
    //     Product newProduct = _productsService.AddProduct(product);
    //     return Ok(newProduct);
    // }


    // Adding a Product
    // only allowed by admin
    [Authorize(Roles ="admin")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var result = await _productService.CreateProduct(request);
        if (result == null)
        {
            return BadRequest(new ApiResponse<ProductDto>(false, "Product Creation Failed", null));
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<ProductDto>(true, "Product Created Successfully", result));
    }

    // [HttpGet]
    // public ActionResult GetAll()
    // {
    //     IEnumerable<Product> products = _productsService.GetAllProducts();
    //     return Ok(products);
    // }

    // Get List of Products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();
        return Ok(new ApiResponse<IEnumerable<ProductDto>>(true, "Products Fetched Successfully", result));
    }

    // GET: http://localhost/Products/1
    // [HttpGet("{id}")]
    // public ActionResult GetProduct(string id)
    // {
    //     Product product = _productsService.GetProduct(id);
    //     return Ok(product);
    // }

    // Get a Product by Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetById(id);
        if (result == null)
        {
            return NotFound(new ApiResponse<ProductDto>(false, "Product Not Found", null));
        }
        return Ok(new ApiResponse<ProductDto>(true, "Product Fetched Successfully", result));
    }

    // Update a Product
    [Authorize(Roles ="admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CreateProductRequest request)
    {
        var result = await _productService.UpdateProduct(id, request);
        if (result == null)
        {
            return NotFound(new ApiResponse<ProductDto>(false, "Product Not Found", null));
        }
        return Ok(new ApiResponse<ProductDto>(true, "Product Updated Successfully", result));
    }

    // Delete a Product
    [Authorize(Roles ="admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await _productService.DeleteProductAsync(id);
        if (!result)
        {
            return NotFound(new ApiResponse<bool>(false, "Product Not Found", false));
        }
        return Ok(new ApiResponse<bool>(true, "Product Deleted Successfully", true));
    }
    // [HttpPut("{id}")]
    // public ActionResult UpdateProduct([FromBody] Product product, string id)
    // {
    //     Product product1 = _productsService.UpdateProduct(id, product);
    //     return Ok(product1);
    // }

    // [HttpDelete("{id}")]
    // public ActionResult DeleteProduct(string id)
    // {
    //     _productsService.DeleteProduct(id);
    //     return NoContent();
    // }
}