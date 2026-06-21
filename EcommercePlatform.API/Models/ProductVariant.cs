namespace EcommercePlatform.API.Models;

public class ProductVariant
{
    public string Id { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string ColorCode { get; set; } = string.Empty; // Hex code for color swatch
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public int Stock { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
