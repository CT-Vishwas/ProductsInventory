using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductsInventory.Api.Services;
using ProductsInventory.Api.Controllers;
using ProductsInventory.Api.Data.Requests;
using ProductsInventory.Api.Data.DTOs;
using ProductsInventory.Api.Data.Responses;

namespace ProductsInventory.Tests.Controllers
{
    public class AuthControllerTestsTest
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTestsTest()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var userRequest = new UserRequest { Username = "user", Password = "pass", Role = "admin" };
            var token = Guid.NewGuid().ToString() + ".jwt.token";
            var userDto = new UserDto
            {
                Id = Guid.NewGuid(),
                Username = userRequest.Username,
                Role = userRequest.Role,
                token = token
            };
            _authServiceMock.Setup(s => s.LoginAsync(userRequest))
                .ReturnsAsync(userDto);

            // Act
            var result = await _controller.Login(userRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<UserDto>>(okResult.Value);
            Assert.Equal(apiResponse, okResult.Value);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var userRequest = new UserRequest { Username = "user", Password = "wrongpass" };
            _authServiceMock.Setup(s => s.LoginAsync(userRequest))
                .ReturnsAsync(null as UserDto);

            // Act
            var result = await _controller.Login(userRequest);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Register_ValidModel_ReturnsOkObjectResult()
        {
            // Arrange
            var userRequest = new UserRequest { Username = "newuser", Password = "pass", Role = "user" };
            _authServiceMock.Setup(s => s.RegisterAsync(userRequest))
                .ReturnsAsync(new UserDto
                {
                    Id = Guid.NewGuid(),
                    Username = userRequest.Username,
                    Role = "user",
                    token = null
                });

            // Act
            var result = await _controller.Register(userRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_InvalidModel_ReturnsBadObjectRequest()
        {
            // Arrange
            var userRequest = new UserRequest { Username = "existinguser", Password = "pass" };
            _authServiceMock.Setup(s => s.RegisterAsync(userRequest))
                .ReturnsAsync(null as UserDto);

            // Act
            var result = await _controller.Register(userRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}