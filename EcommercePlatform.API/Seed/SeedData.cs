using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using MongoDB.Driver;

namespace EcommercePlatform.API.Seed;

public class SeedData
{
    private readonly MongoDbContext _context;

    public SeedData(MongoDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await SeedCategories();
        await SeedSubCategories();
        await SeedProducts();
        await SeedUsers();
        await SeedHeader();
        await SeedFooter();
        await SeedAdvertisements();
    }

    private async Task SeedCategories()
    {
        var categories = new List<Category>
        {
            new Category { Id = "cat1", Name = "Electronics", Description = "Latest electronic devices", ImageUrl = "https://images.unsplash.com/photo-1498049794561-7780e7231661?w=600&h=600&fit=crop", DisplayOrder = 1 },
            new Category { Id = "cat2", Name = "Fashion", Description = "Trending fashion items", ImageUrl = "https://images.unsplash.com/photo-1445205170230-053b83016050?w=600&h=600&fit=crop", DisplayOrder = 2 },
            new Category { Id = "cat3", Name = "Home & Living", Description = "Home decor and essentials", ImageUrl = "https://images.unsplash.com/photo-1556228453-efd6c1ff04f6?w=600&h=600&fit=crop", DisplayOrder = 3 },
            new Category { Id = "cat4", Name = "Sports", Description = "Sports and fitness equipment", ImageUrl = "https://images.unsplash.com/photo-1517836357463-d25dfeac3438?w=600&h=600&fit=crop", DisplayOrder = 4 },
            new Category { Id = "cat5", Name = "Books", Description = "Books and educational materials", ImageUrl = "https://images.unsplash.com/photo-1512820790803-83ca734da794?w=600&h=600&fit=crop", DisplayOrder = 5 }
        };

        await _context.Categories.DeleteManyAsync(FilterDefinition<Category>.Empty);
        await _context.Categories.InsertManyAsync(categories);
    }

    private async Task SeedSubCategories()
    {
        var subCategories = new List<SubCategory>
        {
            new SubCategory { Id = "subcat1", Name = "Smartphones", Description = "Mobile phones and accessories", ParentCategoryId = "cat1", DisplayOrder = 1 },
            new SubCategory { Id = "subcat2", Name = "Laptops", Description = "Laptops and computers", ParentCategoryId = "cat1", DisplayOrder = 2 },
            new SubCategory { Id = "subcat3", Name = "Audio", Description = "Headphones and speakers", ParentCategoryId = "cat1", DisplayOrder = 3 },
            new SubCategory { Id = "subcat4", Name = "Men's Clothing", Description = "Clothing for men", ParentCategoryId = "cat2", DisplayOrder = 1 },
            new SubCategory { Id = "subcat5", Name = "Women's Clothing", Description = "Clothing for women", ParentCategoryId = "cat2", DisplayOrder = 2 },
            new SubCategory { Id = "subcat6", Name = "Footwear", Description = "Shoes and sandals", ParentCategoryId = "cat2", DisplayOrder = 3 },
            new SubCategory { Id = "subcat7", Name = "Furniture", Description = "Living room and bedroom furniture", ParentCategoryId = "cat3", DisplayOrder = 1 },
            new SubCategory { Id = "subcat8", Name = "Decor", Description = "Home decorations", ParentCategoryId = "cat3", DisplayOrder = 2 },
            new SubCategory { Id = "subcat9", Name = "Fitness Equipment", Description = "Gym and fitness gear", ParentCategoryId = "cat4", DisplayOrder = 1 },
            new SubCategory { Id = "subcat10", Name = "Sports Accessories", Description = "Sports accessories", ParentCategoryId = "cat4", DisplayOrder = 2 },
            new SubCategory { Id = "subcat11", Name = "Fiction", Description = "Fiction books", ParentCategoryId = "cat5", DisplayOrder = 1 },
            new SubCategory { Id = "subcat12", Name = "Non-Fiction", Description = "Non-fiction books", ParentCategoryId = "cat5", DisplayOrder = 2 }
        };

        await _context.SubCategories.DeleteManyAsync(FilterDefinition<SubCategory>.Empty);
        await _context.SubCategories.InsertManyAsync(subCategories);
    }

    private async Task SeedProducts()
    {
        var products = new List<Product>
        {
            new Product {
                Id = "prod1",
                Name = "Smartphone Pro",
                Description = "Latest smartphone with advanced features, stunning display, and all-day battery life.",
                CategoryId = "cat1",
                SubCategoryId = "subcat1",
                Price = 1200m,
                OriginalPrice = 1200m,
                DiscountPercentage = 15m,
                Stock = 50,
                IsFeatured = true,
                Rating = 4.5m,
                ReviewCount = 120,
                ImageUrl = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1592899677977-9c10ca588bbd?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?w=700&h=700&fit=crop" },
                AvailableColors = new List<ColorOption> { new ColorOption { Name = "Black", Code = "#000000" }, new ColorOption { Name = "White", Code = "#FFFFFF" }, new ColorOption { Name = "Blue", Code = "#0000FF" } },
                AvailableSizes = new List<string> { "128GB", "256GB", "512GB" },
                Variants = new List<ProductVariant>
                {
                    new ProductVariant { Id = "var1", Color = "Black", ColorCode = "#000000", Size = "128GB", Price = 1200m, OriginalPrice = 1200m, DiscountPercentage = 15m, Stock = 20 },
                    new ProductVariant { Id = "var2", Color = "Black", ColorCode = "#000000", Size = "256GB", Price = 1300m, OriginalPrice = 1300m, DiscountPercentage = 15m, Stock = 15 },
                    new ProductVariant { Id = "var3", Color = "White", ColorCode = "#FFFFFF", Size = "128GB", Price = 1200m, OriginalPrice = 1200m, DiscountPercentage = 15m, Stock = 10 },
                    new ProductVariant { Id = "var4", Color = "Blue", ColorCode = "#0000FF", Size = "256GB", Price = 1300m, OriginalPrice = 1300m, DiscountPercentage = 20m, Stock = 5 }
                },
                Warranty = "1 Year Manufacturer Warranty",
                WarrantyIcon = "🛡️",
                Specifications = new List<ProductSpecification>
                {
                    new ProductSpecification { Name = "Display", Value = "6.7-inch OLED" },
                    new ProductSpecification { Name = "Processor", Value = "A17 Pro Chip" },
                    new ProductSpecification { Name = "RAM", Value = "8GB" },
                    new ProductSpecification { Name = "Storage", Value = "128GB/256GB/512GB" },
                    new ProductSpecification { Name = "Camera", Value = "48MP Triple Camera" },
                    new ProductSpecification { Name = "Battery", Value = "4500mAh" },
                    new ProductSpecification { Name = "OS", Value = "iOS 17" }
                },
                Reviews = new List<ProductReview>
                {
                    new ProductReview { Id = "rev1", UserId = "user2", UserName = "John Doe", Rating = 5, Comment = "Amazing phone! The camera quality is outstanding.", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                    new ProductReview { Id = "rev2", UserId = "user3", UserName = "Jane Smith", Rating = 4, Comment = "Great battery life, but the price is a bit high.", CreatedAt = DateTime.UtcNow.AddDays(-10) },
                    new ProductReview { Id = "rev3", UserId = "user5", UserName = "Alice Williams", Rating = 5, Comment = "Best smartphone I've ever owned. Highly recommend!", CreatedAt = DateTime.UtcNow.AddDays(-15) }
                },
                DynamicSections = new List<ProductDynamicSection>
                {
                    new ProductDynamicSection { Id = "ds1", Title = "Warranty", Content = "1 Year Manufacturer Warranty\nCovers manufacturing defects\nDoes not cover physical damage", Icon = "🛡️", SectionType = "list", DisplayOrder = 0, IsActive = true },
                    new ProductDynamicSection { Id = "ds2", Title = "Shipping Info", Content = "Free shipping on orders over ₹100\nStandard delivery: 5-7 business days\nExpress delivery: 2-3 business days", Icon = "🚚", SectionType = "list", DisplayOrder = 1, IsActive = true },
                    new ProductDynamicSection { Id = "ds3", Title = "Returns", Content = "30-day return policy\nFree returns within 7 days\nItem must be in original condition", Icon = "↩️", SectionType = "list", DisplayOrder = 2, IsActive = true }
                },
                TrustBadges = new List<ProductTrustBadge>
                {
                    new ProductTrustBadge { Id = "tb1", Label = "Free shipping over ₹100", Icon = "🚚", DisplayOrder = 0, IsActive = true },
                    new ProductTrustBadge { Id = "tb2", Label = "Secure payment", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                    new ProductTrustBadge { Id = "tb3", Label = "Easy returns", Icon = "↩️", DisplayOrder = 2, IsActive = true }
                }
            },
            new Product {
                Id = "prod2",
                Name = "Laptop Ultra",
                Description = "High-performance laptop for professionals with a sleek aluminum body.",
                CategoryId = "cat1",
                SubCategoryId = "subcat2",
                Price = 1800m,
                OriginalPrice = 1800m,
                DiscountPercentage = 10m,
                Stock = 30,
                IsFeatured = true,
                Rating = 4.7m,
                ReviewCount = 85,
                ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1525547719571-a2d4ac8945e2?w=700&h=700&fit=crop" },
                AvailableColors = new List<ColorOption> { new ColorOption { Name = "Silver", Code = "#C0C0C0" }, new ColorOption { Name = "Space Gray", Code = "#2D2D2D" } },
                AvailableSizes = new List<string> { "13-inch", "15-inch", "16-inch" },
                Variants = new List<ProductVariant>
                {
                    new ProductVariant { Id = "var5", Color = "Silver", ColorCode = "#C0C0C0", Size = "13-inch", Price = 1800m, OriginalPrice = 1800m, DiscountPercentage = 10m, Stock = 10 },
                    new ProductVariant { Id = "var6", Color = "Silver", ColorCode = "#C0C0C0", Size = "15-inch", Price = 2100m, OriginalPrice = 2100m, DiscountPercentage = 12m, Stock = 12 },
                    new ProductVariant { Id = "var7", Color = "Space Gray", ColorCode = "#2D2D2D", Size = "16-inch", Price = 2400m, OriginalPrice = 2400m, DiscountPercentage = 15m, Stock = 8 }
                },
                Warranty = "2 Year Manufacturer Warranty",
                WarrantyIcon = "🛡️",
                Specifications = new List<ProductSpecification>
                {
                    new ProductSpecification { Name = "Display", Value = "16-inch Retina" },
                    new ProductSpecification { Name = "Processor", Value = "M3 Pro Chip" },
                    new ProductSpecification { Name = "RAM", Value = "16GB Unified Memory" },
                    new ProductSpecification { Name = "Storage", Value = "512GB SSD" },
                    new ProductSpecification { Name = "Graphics", Value = "Integrated GPU" },
                    new ProductSpecification { Name = "Battery", Value = "22-hour battery life" },
                    new ProductSpecification { Name = "OS", Value = "macOS Sonoma" }
                },
                Reviews = new List<ProductReview>
                {
                    new ProductReview { Id = "rev4", UserId = "user2", UserName = "John Doe", Rating = 5, Comment = "Perfect for my work. Fast and reliable.", CreatedAt = DateTime.UtcNow.AddDays(-3) },
                    new ProductReview { Id = "rev5", UserId = "user3", UserName = "Jane Smith", Rating = 4, Comment = "Great laptop, but could use more ports.", CreatedAt = DateTime.UtcNow.AddDays(-7) }
                },
                DynamicSections = new List<ProductDynamicSection>
                {
                    new ProductDynamicSection { Id = "ds4", Title = "Warranty", Content = "2 Year Manufacturer Warranty\nIncludes accidental damage protection\n24/7 technical support", Icon = "🛡️", SectionType = "list", DisplayOrder = 0, IsActive = true },
                    new ProductDynamicSection { Id = "ds5", Title = "What's in the Box", Content = "MacBook Pro\n67W USB-C Power Adapter\nUSB-C to MagSafe 3 Cable", Icon = "📦", SectionType = "list", DisplayOrder = 1, IsActive = true }
                },
                TrustBadges = new List<ProductTrustBadge>
                {
                    new ProductTrustBadge { Id = "tb4", Label = "Free shipping over ₹100", Icon = "🚚", DisplayOrder = 0, IsActive = true },
                    new ProductTrustBadge { Id = "tb5", Label = "Secure payment", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                    new ProductTrustBadge { Id = "tb6", Label = "Easy returns", Icon = "↩️", DisplayOrder = 2, IsActive = true }
                }
            },
            new Product { Id = "prod3", Name = "Wireless Headphones", Description = "Premium noise-cancelling headphones with immersive sound.", CategoryId = "cat1", SubCategoryId = "subcat3", Price = 350m, OriginalPrice = 350m, DiscountPercentage = 15m, Stock = 100, Rating = 4.3m, ReviewCount = 200,
                ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1484704849700-f032a568e944?w=700&h=700&fit=crop" },
                Warranty = "1 Year Warranty",
                WarrantyIcon = "🛡️",
                Specifications = new List<ProductSpecification>
                {
                    new ProductSpecification { Name = "Type", Value = "Over-ear" },
                    new ProductSpecification { Name = "Noise Cancellation", Value = "Active Noise Cancellation" },
                    new ProductSpecification { Name = "Battery Life", Value = "30 hours" },
                    new ProductSpecification { Name = "Connectivity", Value = "Bluetooth 5.3" },
                    new ProductSpecification { Name = "Weight", Value = "250g" }
                },
                Reviews = new List<ProductReview>
                {
                    new ProductReview { Id = "rev6", UserId = "user2", UserName = "John Doe", Rating = 4, Comment = "Great sound quality, ANC works well.", CreatedAt = DateTime.UtcNow.AddDays(-2) },
                    new ProductReview { Id = "rev7", UserId = "user3", UserName = "Jane Smith", Rating = 5, Comment = "Best headphones I've ever used!", CreatedAt = DateTime.UtcNow.AddDays(-8) }
                },
                DynamicSections = new List<ProductDynamicSection>
                {
                    new ProductDynamicSection { Id = "ds6", Title = "Warranty", Content = "1 Year Manufacturer Warranty\nCovers battery and electronics\n30-day money-back guarantee", Icon = "🛡️", SectionType = "list", DisplayOrder = 0, IsActive = true }
                },
                TrustBadges = new List<ProductTrustBadge>
                {
                    new ProductTrustBadge { Id = "tb7", Label = "Free shipping over ₹100", Icon = "🚚", DisplayOrder = 0, IsActive = true },
                    new ProductTrustBadge { Id = "tb8", Label = "Secure payment", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                    new ProductTrustBadge { Id = "tb9", Label = "Easy returns", Icon = "↩️", DisplayOrder = 2, IsActive = true }
                }
            },
            new Product {
                Id = "prod4",
                Name = "Designer Jacket",
                Description = "Stylish designer jacket crafted from premium materials.",
                CategoryId = "cat2",
                SubCategoryId = "subcat4",
                Price = 250m,
                OriginalPrice = 250m,
                DiscountPercentage = 20m,
                Stock = 45,
                IsFeatured = true,
                Rating = 4.6m,
                ReviewCount = 95,
                ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=700&h=700&fit=crop" },
                AvailableColors = new List<ColorOption> { new ColorOption { Name = "Black", Code = "#000000" }, new ColorOption { Name = "Brown", Code = "#8B4513" }, new ColorOption { Name = "Navy", Code = "#000080" } },
                AvailableSizes = new List<string> { "S", "M", "L", "XL" },
                Variants = new List<ProductVariant>
                {
                    new ProductVariant { Id = "var8", Color = "Black", ColorCode = "#000000", Size = "M", Price = 250m, OriginalPrice = 250m, DiscountPercentage = 20m, Stock = 15 },
                    new ProductVariant { Id = "var9", Color = "Black", ColorCode = "#000000", Size = "L", Price = 250m, OriginalPrice = 250m, DiscountPercentage = 20m, Stock = 12 },
                    new ProductVariant { Id = "var10", Color = "Brown", ColorCode = "#8B4513", Size = "M", Price = 250m, OriginalPrice = 250m, DiscountPercentage = 20m, Stock = 10 },
                    new ProductVariant { Id = "var11", Color = "Navy", ColorCode = "#000080", Size = "XL", Price = 270m, OriginalPrice = 270m, DiscountPercentage = 18m, Stock = 8 }
                },
                Warranty = "6 Month Warranty",
                WarrantyIcon = "🛡️",
                Specifications = new List<ProductSpecification>
                {
                    new ProductSpecification { Name = "Material", Value = "100% Cotton" },
                    new ProductSpecification { Name = "Fit", Value = "Regular Fit" },
                    new ProductSpecification { Name = "Care", Value = "Machine Washable" },
                    new ProductSpecification { Name = "Season", Value = "All Season" }
                },
                Reviews = new List<ProductReview>
                {
                    new ProductReview { Id = "rev8", UserId = "user2", UserName = "John Doe", Rating = 5, Comment = "Excellent quality jacket. Very comfortable.", CreatedAt = DateTime.UtcNow.AddDays(-4) },
                    new ProductReview { Id = "rev9", UserId = "user3", UserName = "Jane Smith", Rating = 4, Comment = "Great style, fits perfectly.", CreatedAt = DateTime.UtcNow.AddDays(-12) }
                },
                DynamicSections = new List<ProductDynamicSection>
                {
                    new ProductDynamicSection { Id = "ds7", Title = "Care Instructions", Content = "Machine wash cold\nTumble dry low\nDo not bleach\nIron on low heat if needed", Icon = "🧺", SectionType = "list", DisplayOrder = 0, IsActive = true }
                },
                TrustBadges = new List<ProductTrustBadge>
                {
                    new ProductTrustBadge { Id = "tb10", Label = "Free shipping over ₹100", Icon = "🚚", DisplayOrder = 0, IsActive = true },
                    new ProductTrustBadge { Id = "tb11", Label = "Secure payment", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                    new ProductTrustBadge { Id = "tb12", Label = "Easy returns", Icon = "↩️", DisplayOrder = 2, IsActive = true }
                }
            },
            new Product {
                Id = "prod5",
                Name = "Running Shoes",
                Description = "Comfortable running shoes with responsive cushioning.",
                CategoryId = "cat2",
                SubCategoryId = "subcat6",
                Price = 160m,
                OriginalPrice = 160m,
                DiscountPercentage = 18m,
                Stock = 80,
                Rating = 4.4m,
                ReviewCount = 150,
                ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1460353581641-37baddab0fa2?w=700&h=700&fit=crop" },
                AvailableColors = new List<ColorOption> { new ColorOption { Name = "Red", Code = "#FF0000" }, new ColorOption { Name = "Blue", Code = "#0000FF" }, new ColorOption { Name = "Black", Code = "#000000" }, new ColorOption { Name = "White", Code = "#FFFFFF" } },
                AvailableSizes = new List<string> { "7", "8", "9", "10", "11" },
                Variants = new List<ProductVariant>
                {
                    new ProductVariant { Id = "var12", Color = "Red", ColorCode = "#FF0000", Size = "9", Price = 160m, OriginalPrice = 160m, DiscountPercentage = 18m, Stock = 20 },
                    new ProductVariant { Id = "var13", Color = "Blue", ColorCode = "#0000FF", Size = "9", Price = 160m, OriginalPrice = 160m, DiscountPercentage = 18m, Stock = 18 },
                    new ProductVariant { Id = "var14", Color = "Black", ColorCode = "#000000", Size = "10", Price = 160m, OriginalPrice = 160m, DiscountPercentage = 18m, Stock = 15 },
                    new ProductVariant { Id = "var15", Color = "White", ColorCode = "#FFFFFF", Size = "8", Price = 160m, OriginalPrice = 160m, DiscountPercentage = 18m, Stock = 12 }
                },
                Warranty = "1 Year Warranty",
                WarrantyIcon = "🛡️",
                Specifications = new List<ProductSpecification>
                {
                    new ProductSpecification { Name = "Upper", Value = "Mesh" },
                    new ProductSpecification { Name = "Sole", Value = "Rubber" },
                    new ProductSpecification { Name = "Cushioning", Value = "EVA Foam" },
                    new ProductSpecification { Name = "Weight", Value = "280g per shoe" }
                },
                Reviews = new List<ProductReview>
                {
                    new ProductReview { Id = "rev10", UserId = "user2", UserName = "John Doe", Rating = 5, Comment = "Very comfortable for long runs.", CreatedAt = DateTime.UtcNow.AddDays(-6) },
                    new ProductReview { Id = "rev11", UserId = "user3", UserName = "Jane Smith", Rating = 4, Comment = "Good shoes, but sizing runs small.", CreatedAt = DateTime.UtcNow.AddDays(-14) }
                },
                DynamicSections = new List<ProductDynamicSection>
                {
                    new ProductDynamicSection { Id = "ds8", Title = "Warranty", Content = "1 Year Manufacturer Warranty\nCovers sole separation and upper material defects\n30-day comfort guarantee", Icon = "🛡️", SectionType = "list", DisplayOrder = 0, IsActive = true }
                },
                TrustBadges = new List<ProductTrustBadge>
                {
                    new ProductTrustBadge { Id = "tb13", Label = "Free shipping over ₹100", Icon = "🚚", DisplayOrder = 0, IsActive = true },
                    new ProductTrustBadge { Id = "tb14", Label = "Secure payment", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                    new ProductTrustBadge { Id = "tb15", Label = "Easy returns", Icon = "↩️", DisplayOrder = 2, IsActive = true }
                }
            },
            new Product { Id = "prod6", Name = "Modern Sofa", Description = "Comfortable modern sofa that elevates any living space.", CategoryId = "cat3", SubCategoryId = "subcat7", Price = 1100m, OriginalPrice = 1100m, DiscountPercentage = 18m, Stock = 15, IsFeatured = true, Rating = 4.8m, ReviewCount = 45,
                ImageUrl = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1567016432779-094069958ea5?w=700&h=700&fit=crop" },
                AvailableColors = new List<ColorOption> { new ColorOption { Name = "Gray", Code = "#808080" }, new ColorOption { Name = "Beige", Code = "#F5F5DC" }, new ColorOption { Name = "Navy", Code = "#000080" } },
                AvailableSizes = new List<string> { "2-Seater", "3-Seater", "L-Shape" },
                Variants = new List<ProductVariant>
                {
                    new ProductVariant { Id = "var16", Color = "Gray", ColorCode = "#808080", Size = "3-Seater", Price = 1100m, OriginalPrice = 1100m, DiscountPercentage = 18m, Stock = 8 },
                    new ProductVariant { Id = "var17", Color = "Beige", ColorCode = "#F5F5DC", Size = "3-Seater", Price = 1100m, OriginalPrice = 1100m, DiscountPercentage = 18m, Stock = 5 },
                    new ProductVariant { Id = "var18", Color = "Navy", ColorCode = "#000080", Size = "L-Shape", Price = 1500m, OriginalPrice = 1500m, DiscountPercentage = 13m, Stock = 2 }
                },
                Warranty = "3 Year Warranty",
                WarrantyIcon = "🛡️",
                Specifications = new List<ProductSpecification>
                {
                    new ProductSpecification { Name = "Material", Value = "Premium Fabric" },
                    new ProductSpecification { Name = "Frame", Value = "Solid Wood" },
                    new ProductSpecification { Name = "Dimensions", Value = "84\" W x 36\" D x 34\" H" },
                    new ProductSpecification { Name = "Assembly", Value = "Assembly Required" }
                },
                Reviews = new List<ProductReview>
                {
                    new ProductReview { Id = "rev12", UserId = "user2", UserName = "John Doe", Rating = 5, Comment = "Beautiful sofa, very comfortable.", CreatedAt = DateTime.UtcNow.AddDays(-7) },
                    new ProductReview { Id = "rev13", UserId = "user3", UserName = "Jane Smith", Rating = 5, Comment = "Perfect for our living room!", CreatedAt = DateTime.UtcNow.AddDays(-20) }
                },
                DynamicSections = new List<ProductDynamicSection>
                {
                    new ProductDynamicSection { Id = "ds9", Title = "Warranty", Content = "3 Year Manufacturer Warranty\nCovers frame and fabric\nFree repair service included", Icon = "🛡️", SectionType = "list", DisplayOrder = 0, IsActive = true },
                    new ProductDynamicSection { Id = "ds10", Title = "Delivery", Content = "Free white-glove delivery\nAssembly included\nRemoval of old furniture available", Icon = "🚚", SectionType = "list", DisplayOrder = 1, IsActive = true }
                },
                TrustBadges = new List<ProductTrustBadge>
                {
                    new ProductTrustBadge { Id = "tb16", Label = "Free white-glove delivery", Icon = "🚚", DisplayOrder = 0, IsActive = true },
                    new ProductTrustBadge { Id = "tb17", Label = "Secure payment", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                    new ProductTrustBadge { Id = "tb18", Label = "Easy returns", Icon = "↩️", DisplayOrder = 2, IsActive = true }
                }
            },
            new Product { Id = "prod7", Name = "Yoga Mat", Description = "Premium non-slip yoga mat for your daily practice.", CategoryId = "cat4", SubCategoryId = "subcat9", Price = 60m, OriginalPrice = 60m, DiscountPercentage = 16m, Stock = 200, Rating = 4.2m, ReviewCount = 300,
                ImageUrl = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1518611012118-696072aa579a?w=700&h=700&fit=crop" },
                AvailableColors = new List<ColorOption> { new ColorOption { Name = "Purple", Code = "#800080" }, new ColorOption { Name = "Blue", Code = "#0000FF" }, new ColorOption { Name = "Pink", Code = "#FFC0CB" }, new ColorOption { Name = "Green", Code = "#008000" } },
                AvailableSizes = new List<string> { "Standard", "Extra Long" },
                Variants = new List<ProductVariant>
                {
                    new ProductVariant { Id = "var19", Color = "Purple", ColorCode = "#800080", Size = "Standard", Price = 60m, OriginalPrice = 60m, DiscountPercentage = 16m, Stock = 60 },
                    new ProductVariant { Id = "var20", Color = "Blue", ColorCode = "#0000FF", Size = "Standard", Price = 60m, OriginalPrice = 60m, DiscountPercentage = 16m, Stock = 55 },
                    new ProductVariant { Id = "var21", Color = "Pink", ColorCode = "#FFC0CB", Size = "Extra Long", Price = 70m, OriginalPrice = 70m, DiscountPercentage = 14m, Stock = 40 }
                },
                Warranty = "6 Month Warranty",
                WarrantyIcon = "🛡️",
                Specifications = new List<ProductSpecification>
                {
                    new ProductSpecification { Name = "Material", Value = "Eco-friendly TPE" },
                    new ProductSpecification { Name = "Thickness", Value = "6mm" },
                    new ProductSpecification { Name = "Dimensions", Value = "72\" x 24\"" },
                    new ProductSpecification { Name = "Weight", Value = "2.5 lbs" }
                },
                Reviews = new List<ProductReview>
                {
                    new ProductReview { Id = "rev14", UserId = "user2", UserName = "John Doe", Rating = 4, Comment = "Good grip, comfortable thickness.", CreatedAt = DateTime.UtcNow.AddDays(-3) },
                    new ProductReview { Id = "rev15", UserId = "user3", UserName = "Jane Smith", Rating = 5, Comment = "Best yoga mat I've used!", CreatedAt = DateTime.UtcNow.AddDays(-10) }
                },
                DynamicSections = new List<ProductDynamicSection>
                {
                    new ProductDynamicSection { Id = "ds11", Title = "Warranty", Content = "6 Month Warranty\nCovers material defects\nFree replacement if tears within 30 days", Icon = "🛡️", SectionType = "list", DisplayOrder = 0, IsActive = true }
                },
                TrustBadges = new List<ProductTrustBadge>
                {
                    new ProductTrustBadge { Id = "tb19", Label = "Free shipping over ₹100", Icon = "🚚", DisplayOrder = 0, IsActive = true },
                    new ProductTrustBadge { Id = "tb20", Label = "Secure payment", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                    new ProductTrustBadge { Id = "tb21", Label = "Easy returns", Icon = "↩️", DisplayOrder = 2, IsActive = true }
                }
            },
            new Product { Id = "prod8", Name = "Bestseller Novel", Description = "Popular fiction novel everyone is talking about.", CategoryId = "cat5", SubCategoryId = "subcat11", Price = 25m, OriginalPrice = 25m, DiscountPercentage = 20m, Stock = 150, IsFeatured = true, Rating = 4.9m, ReviewCount = 500,
                ImageUrl = "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1512820790803-83ca734da794?w=700&h=700&fit=crop" },
                Warranty = "N/A",
                WarrantyIcon = "",
                Specifications = new List<ProductSpecification>
                {
                    new ProductSpecification { Name = "Format", Value = "Hardcover" },
                    new ProductSpecification { Name = "Pages", Value = "400" },
                    new ProductSpecification { Name = "Language", Value = "English" },
                    new ProductSpecification { Name = "Publisher", Value = "Best Books Inc." }
                },
                Reviews = new List<ProductReview>
                {
                    new ProductReview { Id = "rev16", UserId = "user2", UserName = "John Doe", Rating = 5, Comment = "Couldn't put it down! Amazing story.", CreatedAt = DateTime.UtcNow.AddDays(-1) },
                    new ProductReview { Id = "rev17", UserId = "user3", UserName = "Jane Smith", Rating = 5, Comment = "Highly recommend this book.", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                    new ProductReview { Id = "rev18", UserId = "user5", UserName = "Alice Williams", Rating = 5, Comment = "Best book I've read this year!", CreatedAt = DateTime.UtcNow.AddDays(-15) }
                },
                DynamicSections = new List<ProductDynamicSection>
                {
                    new ProductDynamicSection { Id = "ds12", Title = "About the Author", Content = "New York Times bestselling author\nAward-winning novelist\nKnown for gripping thrillers", Icon = "✍️", SectionType = "list", DisplayOrder = 0, IsActive = true }
                },
                TrustBadges = new List<ProductTrustBadge>
                {
                    new ProductTrustBadge { Id = "tb22", Label = "Free shipping over ₹100", Icon = "🚚", DisplayOrder = 0, IsActive = true },
                    new ProductTrustBadge { Id = "tb23", Label = "Secure payment", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                    new ProductTrustBadge { Id = "tb24", Label = "Easy returns", Icon = "↩️", DisplayOrder = 2, IsActive = true }
                }
            }
        };

        await _context.Products.DeleteManyAsync(FilterDefinition<Product>.Empty);
        await _context.Products.InsertManyAsync(products);
    }

    private async Task SeedUsers()
    {
        var existingUsers = await _context.Users.CountDocumentsAsync(FilterDefinition<User>.Empty);
        if (existingUsers > 0) return;

        var users = new List<User>
        {
            new User
            {
                Id = "user1",
                FullName = "Admin User",
                Email = "admin@eshop.com",
                PhoneNumber = "+1234567890",
                Address = "123 Admin Street",
                City = "New York",
                State = "NY",
                ZipCode = "10001",
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                LastLoginAt = DateTime.UtcNow
            },
            new User
            {
                Id = "user2",
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567891",
                Address = "456 Oak Avenue",
                City = "Los Angeles",
                State = "CA",
                ZipCode = "90001",
                Role = "Customer",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                LastLoginAt = DateTime.UtcNow.AddDays(-1)
            },
            new User
            {
                Id = "user3",
                FullName = "Jane Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "+1234567892",
                Address = "789 Pine Road",
                City = "Chicago",
                State = "IL",
                ZipCode = "60601",
                Role = "Customer",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                LastLoginAt = DateTime.UtcNow.AddDays(-2)
            },
            new User
            {
                Id = "user4",
                FullName = "Bob Johnson",
                Email = "bob.johnson@example.com",
                PhoneNumber = "+1234567893",
                Address = "321 Elm Street",
                City = "Houston",
                State = "TX",
                ZipCode = "77001",
                Role = "Customer",
                IsActive = false,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                LastLoginAt = DateTime.UtcNow.AddDays(-3)
            },
            new User
            {
                Id = "user5",
                FullName = "Alice Williams",
                Email = "alice.williams@example.com",
                PhoneNumber = "+1234567894",
                Address = "654 Maple Drive",
                City = "Phoenix",
                State = "AZ",
                ZipCode = "85001",
                Role = "Customer",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                LastLoginAt = DateTime.UtcNow.AddDays(-1)
            },
            new User
            {
                Id = "user6",
                FullName = "Marketing Manager",
                Email = "marketing@eshop.com",
                PhoneNumber = "+1234567895",
                Address = "999 Marketing Blvd",
                City = "San Francisco",
                State = "CA",
                ZipCode = "94102",
                Role = "Advertiser",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                LastLoginAt = DateTime.UtcNow
            },
            new User
            {
                Id = "user7",
                FullName = "Brand Manager",
                Email = "brand@eshop.com",
                PhoneNumber = "+1234567896",
                Address = "888 Brand Avenue",
                City = "Seattle",
                State = "WA",
                ZipCode = "98101",
                Role = "Advertiser",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-12),
                LastLoginAt = DateTime.UtcNow.AddDays(-1)
            },
            new User
            {
                Id = "user8",
                FullName = "Content Creator",
                Email = "content@eshop.com",
                PhoneNumber = "+1234567897",
                Address = "777 Creative Lane",
                City = "Austin",
                State = "TX",
                ZipCode = "78701",
                Role = "Other",
                CustomRole = "Content Creator",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                LastLoginAt = DateTime.UtcNow.AddDays(-2)
            },
            new User
            {
                Id = "user9",
                FullName = "Support Lead",
                Email = "support@eshop.com",
                PhoneNumber = "+1234567898",
                Address = "666 Support Street",
                City = "Denver",
                State = "CO",
                ZipCode = "80201",
                Role = "Other",
                CustomRole = "Support Lead",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-25),
                LastLoginAt = DateTime.UtcNow
            },
            new User
            {
                Id = "user10",
                FullName = "Warehouse Manager",
                Email = "warehouse@eshop.com",
                PhoneNumber = "+1234567899",
                Address = "555 Warehouse Road",
                City = "Miami",
                State = "FL",
                ZipCode = "33101",
                Role = "Other",
                CustomRole = "Warehouse Manager",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-18),
                LastLoginAt = DateTime.UtcNow.AddDays(-1)
            }
        };

        await _context.Users.InsertManyAsync(users);
    }

    private async Task SeedHeader()
    {
        var existingHeaders = await _context.Headers.CountDocumentsAsync(FilterDefinition<Header>.Empty);
        if (existingHeaders > 0) return;

        var header = new Header
        {
            Id = "header1",
            Title = "E-Shop",
            LogoUrl = "/logo.png",
            MenuItems = new List<MenuItem>
            {
                new MenuItem { Id = "menu1", Label = "Home", Link = "/", DisplayOrder = 1 },
                new MenuItem { Id = "menu2", Label = "Shopping", Link = "/shopping", DisplayOrder = 2 },
                new MenuItem { Id = "menu3", Label = "Categories", Link = "/categories", DisplayOrder = 3, SubMenus = new List<MenuItem>
                {
                    new MenuItem { Id = "submenu1", Label = "Electronics", Link = "/categories/electronics", DisplayOrder = 1 },
                    new MenuItem { Id = "submenu2", Label = "Fashion", Link = "/categories/fashion", DisplayOrder = 2 },
                    new MenuItem { Id = "submenu3", Label = "Home & Living", Link = "/categories/home", DisplayOrder = 3 }
                }},
                new MenuItem { Id = "menu4", Label = "Advertisements", Link = "/advertisements", DisplayOrder = 4 },
                new MenuItem { Id = "menu5", Label = "Contact", Link = "/contact", DisplayOrder = 5 }
            },
            ShowCartIcon = true,
            ShowUserIcon = true,
            ShowSearchIcon = true
        };

        await _context.Headers.InsertOneAsync(header);
    }

    private async Task SeedFooter()
    {
        await _context.Footers.DeleteManyAsync(FilterDefinition<Footer>.Empty);

        var footer = new Footer
        {
            Id = "footer1",
            CompanyName = "E-Shop Inc.",
            Description = "Your one-stop shop for everything",
            CopyrightText = "© 2024 E-Shop Inc. All rights reserved.",
            SocialLinks = new List<SocialMediaLink>
            {
                new SocialMediaLink { Id = "social1", Name = "Facebook", Url = "https://facebook.com/eshop", DisplayOrder = 1, IsActive = true },
                new SocialMediaLink { Id = "social2", Name = "Twitter", Url = "https://twitter.com/eshop", DisplayOrder = 2, IsActive = true },
                new SocialMediaLink { Id = "social3", Name = "Instagram", Url = "https://instagram.com/eshop", DisplayOrder = 3, IsActive = true },
                new SocialMediaLink { Id = "social4", Name = "LinkedIn", Url = "https://linkedin.com/company/eshop", DisplayOrder = 4, IsActive = true }
            },
            ContactInfo = new ContactInfo
            {
                Id = "contact1",
                Email = "contact@eshop.com",
                Phone = "+1 234 567 890",
                Address = "123 Main Street",
                City = "New York",
                State = "NY",
                ZipCode = "10001",
                Country = "USA"
            },
            Sections = new List<FooterSection>
            {
                new FooterSection
                {
                    Id = "section1",
                    Title = "Quick Links",
                    Links = new List<FooterLink>
                    {
                        new FooterLink { Id = "link1", Label = "Home", Link = "/", DisplayOrder = 1 },
                        new FooterLink { Id = "link2", Label = "About Us", Link = "/about", DisplayOrder = 2 },
                        new FooterLink { Id = "link3", Label = "Contact", Link = "/contact", DisplayOrder = 3 }
                    }
                },
                new FooterSection
                {
                    Id = "section2",
                    Title = "Customer Service",
                    Links = new List<FooterLink>
                    {
                        new FooterLink { Id = "link4", Label = "FAQ", Link = "/faq", DisplayOrder = 1 },
                        new FooterLink { Id = "link5", Label = "Shipping", Link = "/shipping", DisplayOrder = 2 },
                        new FooterLink { Id = "link6", Label = "Returns", Link = "/returns", DisplayOrder = 3 }
                    }
                },
                new FooterSection
                {
                    Id = "section3",
                    Title = "Categories",
                    Links = new List<FooterLink>
                    {
                        new FooterLink { Id = "link7", Label = "Electronics", Link = "/categories/electronics", DisplayOrder = 1 },
                        new FooterLink { Id = "link8", Label = "Fashion", Link = "/categories/fashion", DisplayOrder = 2 },
                        new FooterLink { Id = "link9", Label = "Home & Living", Link = "/categories/home", DisplayOrder = 3 }
                    }
                }
            }
        };

        await _context.Footers.InsertOneAsync(footer);
    }

    private async Task SeedAdvertisements()
    {
        var existingAds = await _context.Advertisements.CountDocumentsAsync(FilterDefinition<Advertisement>.Empty);
        if (existingAds > 0) return;

        var advertisements = new List<Advertisement>
        {
            new Advertisement
            {
                Id = "ad1",
                Title = "Summer Sale",
                Description = "Up to 50% off on all items",
                ImageUrl = "/ads/summer-sale.jpg",
                Link = "/sale",
                Position = AdPosition.HomeBanner,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                DisplayOrder = 1
            },
            new Advertisement
            {
                Id = "ad2",
                Title = "New Arrivals",
                Description = "Check out our latest collection",
                ImageUrl = "/ads/new-arrivals.jpg",
                Link = "/new-arrivals",
                Position = AdPosition.Sidebar,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                DisplayOrder = 2
            },
            new Advertisement
            {
                Id = "ad3",
                Title = "Electronics Special",
                Description = "Best deals on electronics",
                ImageUrl = "/ads/electronics.jpg",
                Link = "/categories/electronics",
                CategoryId = "cat1",
                Position = AdPosition.BetweenProducts,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                DisplayOrder = 3
            }
        };

        await _context.Advertisements.InsertManyAsync(advertisements);
    }
}
