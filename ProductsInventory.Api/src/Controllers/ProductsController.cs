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

    // Get List of Products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();
        return Ok(new ApiResponse<IEnumerable<ProductDto>>(true, "Products Fetched Successfully", result));
    }


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
}