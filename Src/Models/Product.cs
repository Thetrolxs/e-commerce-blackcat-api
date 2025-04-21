using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public bool IsNew { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //Relaciones
    public ICollection<CartItem> CartItems { get; set; } = null!;
    public ICollection<OrderDetail> OrderDetails { get; set; } = null!;

}