namespace ProductsInventory.Api.Data.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    // Once stored in the database, this will be used to identify the user.
    public required string Username { get; set; }

    public required string Role { get; set; }
    public string? token { get; set; }
}