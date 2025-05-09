using System;

namespace e_commerce_blackcat_api.Src.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public bool IsNew { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
