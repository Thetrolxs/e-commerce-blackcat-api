namespace e_commerce_blackcat_api.Helpers;

public class ProductParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public bool? IsNew { get; set; }
    public string? Sort { get; set; }

    // Opcional para ordenamiento por precio
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    private const int MaxPageSize = 50;
    public int ValidatedPageSize => PageSize > MaxPageSize ? MaxPageSize : PageSize;
}
