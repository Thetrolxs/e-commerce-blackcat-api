using System.ComponentModel.DataAnnotations;

using e_commerce_blackcat_api.Src.Helpers;

namespace e_commerce_blackcat_api.Src.Dtos;

public class ProductCreateDto
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

    [AllowedExtensionsAndMimeTypes(new[] { ".png", ".jpg", ".jpeg", "webp" }, new[] { "image/png", "image/jpeg", "image/jpg", "image/webp" })]
    [MaxFileSize(100 * 1024 * 1024, ErrorMessage = "El tamaño máximo del archivo es 100 MB")]
    public required IFormFile? Image { get; set; }

}
