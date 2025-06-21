namespace ProductsInventory.Api.Services
{
    using ProductsInventory.Api.Data.DTOs;
    using ProductsInventory.Api.Data.Requests;

    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(UserRequest userRequest);
        Task<UserDto> LoginAsync(UserRequest userRequest);
        Task<bool> IsUserExistsAsync(string username);
        Task<UserDto> GetUserByUsernameAsync(string username);

        Task<UserDto> GetById(Guid id);
    }
}