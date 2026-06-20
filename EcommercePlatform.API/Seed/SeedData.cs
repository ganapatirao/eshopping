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
        await SeedProducts();
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

    private async Task SeedProducts()
    {
        var products = new List<Product>
        {
            new Product { Id = "prod1", Name = "Smartphone Pro", Description = "Latest smartphone with advanced features, stunning display, and all-day battery life.", CategoryId = "cat1", SubCategoryId = "cat1", Price = 999.99m, OriginalPrice = 1199.99m, Stock = 50, IsFeatured = true, Rating = 4.5m, ReviewCount = 120,
                ImageUrl = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1592899677977-9c10ca588bbd?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?w=700&h=700&fit=crop" } },
            new Product { Id = "prod2", Name = "Laptop Ultra", Description = "High-performance laptop for professionals with a sleek aluminum body.", CategoryId = "cat1", SubCategoryId = "cat1", Price = 1499.99m, OriginalPrice = 1799.99m, Stock = 30, IsFeatured = true, Rating = 4.7m, ReviewCount = 85,
                ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1525547719571-a2d4ac8945e2?w=700&h=700&fit=crop" } },
            new Product { Id = "prod3", Name = "Wireless Headphones", Description = "Premium noise-cancelling headphones with immersive sound.", CategoryId = "cat1", SubCategoryId = "cat1", Price = 299.99m, Stock = 100, Rating = 4.3m, ReviewCount = 200,
                ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1484704849700-f032a568e944?w=700&h=700&fit=crop" } },
            new Product { Id = "prod4", Name = "Designer Jacket", Description = "Stylish designer jacket crafted from premium materials.", CategoryId = "cat2", SubCategoryId = "cat2", Price = 199.99m, OriginalPrice = 249.99m, Stock = 45, IsFeatured = true, Rating = 4.6m, ReviewCount = 95,
                ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=700&h=700&fit=crop" } },
            new Product { Id = "prod5", Name = "Running Shoes", Description = "Comfortable running shoes with responsive cushioning.", CategoryId = "cat2", SubCategoryId = "cat2", Price = 129.99m, Stock = 80, Rating = 4.4m, ReviewCount = 150,
                ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1460353581641-37baddab0fa2?w=700&h=700&fit=crop" } },
            new Product { Id = "prod6", Name = "Modern Sofa", Description = "Comfortable modern sofa that elevates any living space.", CategoryId = "cat3", SubCategoryId = "cat3", Price = 899.99m, OriginalPrice = 1099.99m, Stock = 15, IsFeatured = true, Rating = 4.8m, ReviewCount = 45,
                ImageUrl = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1567016432779-094069958ea5?w=700&h=700&fit=crop" } },
            new Product { Id = "prod7", Name = "Yoga Mat", Description = "Premium non-slip yoga mat for your daily practice.", CategoryId = "cat4", SubCategoryId = "cat4", Price = 49.99m, Stock = 200, Rating = 4.2m, ReviewCount = 300,
                ImageUrl = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1518611012118-696072aa579a?w=700&h=700&fit=crop" } },
            new Product { Id = "prod8", Name = "Bestseller Novel", Description = "Popular fiction novel everyone is talking about.", CategoryId = "cat5", SubCategoryId = "cat5", Price = 19.99m, Stock = 150, IsFeatured = true, Rating = 4.9m, ReviewCount = 500,
                ImageUrl = "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=700&h=700&fit=crop",
                ImageUrls = new List<string> { "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=700&h=700&fit=crop", "https://images.unsplash.com/photo-1512820790803-83ca734da794?w=700&h=700&fit=crop" } }
        };

        await _context.Products.DeleteManyAsync(FilterDefinition<Product>.Empty);
        await _context.Products.InsertManyAsync(products);
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
        var existingFooters = await _context.Footers.CountDocumentsAsync(FilterDefinition<Footer>.Empty);
        if (existingFooters > 0) return;

        var footer = new Footer
        {
            Id = "footer1",
            CompanyName = "E-Shop Inc.",
            Description = "Your one-stop shop for everything",
            CopyrightText = "© 2024 E-Shop Inc. All rights reserved.",
            SocialLinks = new List<string> { "https://facebook.com/eshop", "https://twitter.com/eshop", "https://instagram.com/eshop" },
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
