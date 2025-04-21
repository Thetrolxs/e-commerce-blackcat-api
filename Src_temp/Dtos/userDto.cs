namespace e_commerce_blackcat_api.Src.Dtos;

public class UserDto
{
    public required string Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }

    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Commune { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
}
