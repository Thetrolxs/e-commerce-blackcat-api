using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Src.Models;

public class ShippingAddres
{
    public int Id { get; set; }
    public required string Street { get; set; }
    public required string Number { get; set; }
    public required string Commune { get; set; }
    public required string Region { get; set; }
    public required string PostalCode { get; set; }

    // Relación con User
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;
}
