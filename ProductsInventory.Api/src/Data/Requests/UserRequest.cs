namespace ProductsInventory.Api.Data.Requests;
public class UserRequest
{
    public required string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}