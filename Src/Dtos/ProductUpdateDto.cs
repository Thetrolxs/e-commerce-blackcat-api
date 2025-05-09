using System.ComponentModel.DataAnnotations;

namespace e_commerce_blackcat_api.Src.Dtos;

public class ProductUpdateDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public string Brand { get; set; } = string.Empty;

    public bool IsNew { get; set; }
}
