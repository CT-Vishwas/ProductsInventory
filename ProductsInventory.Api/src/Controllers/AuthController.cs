
using Microsoft.AspNetCore.Mvc;
using ProductsInventory.Api.Data.DTOs;
using ProductsInventory.Api.Data.Requests;
using ProductsInventory.Api.Data.Responses;
using ProductsInventory.Api.Services;

namespace  ProductsInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (result == null)
        {
            return BadRequest(new ApiResponse<UserDto>(false, "User Registration Failed", null));
        }
        return Ok("User Registered Successfully");

    }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            var result = await _authService.LoginAsync(userRequest);
            if(result == null)
            {
                return Unauthorized(new ApiResponse<UserDto>(false, "Invalid Credentials", null));
            }
            return Ok(new ApiResponse<UserDto>(true, "Login Successful", result));
        }

}