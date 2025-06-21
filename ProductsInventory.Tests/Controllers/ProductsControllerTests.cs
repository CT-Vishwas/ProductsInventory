using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductsInventory.Api.Services;
using ProductsInventory.Api.Controllers;
using ProductsInventory.Api.Entities;
using ProductsInventory.Api.Data.DTOs;
using Xunit.Sdk;
using ProductsInventory.Api.Data.Responses;
using ProductsInventory.Api.Data.Requests;

namespace ProductsInventory.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsService> _mockService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockService = new Mock<IProductsService>();
            _controller = new ProductsController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var data = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 1" },
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 2" }
            };

            _mockService.Setup(s => s.GetAll()).ReturnsAsync(data);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
             // 2. Check if the value of the OkResult is an ApiResponse<List<ProductDto>>
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<IEnumerable<ProductDto>>>(okResult.Value);
            // 3. Assert on the properties of the ApiResponse
            Assert.True(apiResponse.Success);
            Assert.Equal("Products Fetched Successfully", apiResponse.Message);
            Assert.Null(apiResponse.Errors);

            // 4. Assert on the data returned
            Assert.NotNull(apiResponse.Data);
            Assert.Equal(data.Count, apiResponse.Data.ToList<ProductDto>().Count);
            Assert.Equal(data[0].Name, apiResponse.Data.ToList<ProductDto>()[0].Name);
        }

        [Fact]
        public async Task GetById_ProductExists_ReturnsOkResult()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            var productDto = new ProductDto { Id = productId, Name = "Test" };
            _mockService.Setup(s => s.GetById(productId)).ReturnsAsync(productDto);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            // 2. Check if the value of the OkResult is an ApiResponse<List<ProductDto>>
            var apiResponse = Assert.IsAssignableFrom<ApiResponse<ProductDto>>(okResult.Value);
            // 3. Assert on the properties of the ApiResponse
            Assert.True(apiResponse.Success);
            Assert.Equal("Product Fetched Successfully", apiResponse.Message);
            Assert.Null(apiResponse.Errors);
            Assert.Equal(apiResponse, okResult.Value);
        }

        [Fact]
        public async Task GetById_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            _mockService.Setup(s => s.GetById(productId)).ReturnsAsync(null as ProductDto);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Create_ValidProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            var createRequest = new CreateProductRequest { Name = "New Product" };
            var productDto = new ProductDto { Id = productId, Name = "New Product" };
            _mockService.Setup(s => s.CreateProduct(createRequest)).ReturnsAsync(productDto);

            // Act
            var result = await _controller.CreateProduct(createRequest);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var response = Assert.IsType<ApiResponse<ProductDto>>(createdAtActionResult.Value);
            Assert.Equal("New Product", response.Data.Name);
        }


        [Fact]
        public async Task Create_InvalidProduct_ReturnsBadRequest()
        {
            // Arrange
            var createRequest = new CreateProductRequest { Name = "" }; // Invalid request
            _mockService.Setup(s => s.CreateProduct(createRequest)).ReturnsAsync(null as ProductDto);

            // Act
            var result = await _controller.CreateProduct(createRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOkResult()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            var updateRequest = new CreateProductRequest { Name = "Updated" };
            var product = new ProductDto { Id = productId, Name = "Updated" };
            _mockService.Setup(s => s.UpdateProduct(productId, updateRequest)).ReturnsAsync(product);

            // Act
            var result = await _controller.UpdateProduct(productId, updateRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ApiResponse<ProductDto>>(((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task Update_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            var updateRequest = new ProductsInventory.Api.Data.Requests.CreateProductRequest { Name = "Updated" };
            var product = new ProductDto { Id = productId, Name = "Updated" };

            _mockService.Setup(s => s.UpdateProduct(productId, updateRequest)).ReturnsAsync(null as ProductDto);

            // Act
            var result = await _controller.UpdateProduct(productId, updateRequest);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ProductExists_ReturnsNoContent()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteProductAsync(productId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteProductAsync(productId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}