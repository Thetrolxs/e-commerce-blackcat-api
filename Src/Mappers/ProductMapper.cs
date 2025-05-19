using e_commerce_blackcat_api.Models;
using e_commerce_blackcat_api.Src.Dtos;


namespace e_commerce_blackcat_api.Src.Mappers;

public static class ProductMapper
{
    public static ProductDto ToProductDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category,
            Brand = product.Brand,
            IsNew = product.IsNew,
            CreatedAt = product.CreatedAt,
            Image = product.Image
        };
    }

    public static Product ToProductFromCreateDto(this ProductCreateDto dto, string imageUrl)
    {
        return new Product
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            Category = dto.Category,
            Brand = dto.Brand,
            IsNew = dto.IsNew,
            CreatedAt = DateTime.UtcNow,
            Image = imageUrl
        };
    }

    public static void UpdateProductFromDto(this Product product, ProductUpdateDto dto)
    {
        product.Title = dto.Title;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.Category = dto.Category;
        product.Brand = dto.Brand;
        product.IsNew = dto.IsNew;
    }
}
