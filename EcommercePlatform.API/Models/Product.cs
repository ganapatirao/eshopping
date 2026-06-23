namespace EcommercePlatform.API.Models;

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CategoryId { get; set; } = string.Empty;
    public string SubCategoryId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public decimal DiscountPercentage { get; set; } = 0; // Product-level discount percentage
    public string? ImageUrl { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public int Stock { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; }
    public decimal Rating { get; set; }
    public int ReviewCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Product variants (color/size combinations)
    public List<ProductVariant> Variants { get; set; } = new();

    // Available colors and sizes for this product
    public List<ColorOption> AvailableColors { get; set; } = new();
    public List<string> AvailableSizes { get; set; } = new();

    // New fields for enhanced product information
    public string? Warranty { get; set; } // Warranty information (e.g., "1 year warranty")
    public string? WarrantyIcon { get; set; } // Warranty icon (emoji or image URL)
    public List<ProductSpecification> Specifications { get; set; } = new(); // Product specifications
    public List<ProductReview> Reviews { get; set; } = new(); // Product reviews
    public List<ProductDynamicSection> DynamicSections { get; set; } = new(); // Dynamic sections for flexible content
    public List<ProductTrustBadge> TrustBadges { get; set; } = new(); // Trust badges (free shipping, secure payment, etc.)
}

public class ProductTrustBadge
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty; // e.g., "Free shipping over ₹100"
    public string Icon { get; set; } = string.Empty; // Icon (emoji or image URL)
    public int DisplayOrder { get; set; } = 0; // Order to display badges
    public bool IsActive { get; set; } = true;
}

public class ProductDynamicSection
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty; // e.g., "Warranty", "Shipping Info", "Returns"
    public string Content { get; set; } = string.Empty; // The content of the section
    public string Icon { get; set; } = string.Empty; // Icon name or emoji
    public string SectionType { get; set; } = "text"; // text, list, table, etc.
    public int DisplayOrder { get; set; } = 0; // Order to display sections
    public bool IsActive { get; set; } = true;
}

public class ProductSpecification
{
    public string Name { get; set; } = string.Empty; // e.g., "Screen Size", "Battery"
    public string Value { get; set; } = string.Empty; // e.g., "6.5 inches", "5000mAh"
}

public class ProductReview
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int Rating { get; set; } // 1-5 stars
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
