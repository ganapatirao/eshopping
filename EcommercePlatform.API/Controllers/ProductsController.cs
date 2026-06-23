using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly MongoDbContext _context;

    public ProductsController(MongoDbContext context)
    {
        _context = context;
    }

    private Product RoundProductPrices(Product product)
    {
        product.Price = Math.Round(product.Price);
        if (product.OriginalPrice.HasValue)
        {
            product.OriginalPrice = Math.Round(product.OriginalPrice.Value);
        }
        foreach (var variant in product.Variants)
        {
            variant.Price = Math.Round(variant.Price);
            if (variant.OriginalPrice.HasValue)
            {
                variant.OriginalPrice = Math.Round(variant.OriginalPrice.Value);
            }
        }
        return product;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? categoryId = null, [FromQuery] string? subCategoryId = null)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.IsActive, true);

        if (!string.IsNullOrEmpty(categoryId))
        {
            filter = filter & Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        }

        if (!string.IsNullOrEmpty(subCategoryId))
        {
            filter = filter & Builders<Product>.Filter.Eq(p => p.SubCategoryId, subCategoryId);
        }

        var products = await _context.Products
            .Find(filter)
            .ToListAsync();
        var roundedProducts = products.Select(p => RoundProductPrices(p)).ToList();
        return Ok(roundedProducts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        var product = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (product == null) return NotFound();
        return Ok(RoundProductPrices(product));
    }

    [HttpGet("featured")]
    public async Task<ActionResult<IEnumerable<Product>>> GetFeaturedProducts()
    {
        var products = await _context.Products
            .Find(p => p.IsActive && p.IsFeatured)
            .ToListAsync();
        var roundedProducts = products.Select(p => RoundProductPrices(p)).ToList();
        return Ok(roundedProducts);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryId)
    {
        var products = await _context.Products
            .Find(p => p.IsActive && p.CategoryId == categoryId)
            .ToListAsync();
        var roundedProducts = products.Select(p => RoundProductPrices(p)).ToList();
        return Ok(roundedProducts);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _context.Products
            .Find(FilterDefinition<Product>.Empty)
            .ToListAsync();
        var roundedProducts = products.Select(p => RoundProductPrices(p)).ToList();
        return Ok(roundedProducts);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        if (string.IsNullOrEmpty(product.Id))
            product.Id = Guid.NewGuid().ToString();
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;
        await _context.Products.InsertOneAsync(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, Product product)
    {
        var existing = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (existing == null) return NotFound();

        product.Id = id;
        product.CreatedAt = existing.CreatedAt;
        product.UpdatedAt = DateTime.UtcNow;
        
        // Update variant timestamps
        foreach (var variant in product.Variants)
        {
            if (string.IsNullOrEmpty(variant.Id))
            {
                variant.Id = Guid.NewGuid().ToString();
                variant.CreatedAt = DateTime.UtcNow;
            }
            variant.UpdatedAt = DateTime.UtcNow;
        }
        
        await _context.Products.ReplaceOneAsync(p => p.Id == id, product);
        return NoContent();
    }

    [HttpPost("{productId}/variants")]
    public async Task<ActionResult<ProductVariant>> AddVariant(string productId, ProductVariant variant)
    {
        var product = await _context.Products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        if (product == null) return NotFound();

        variant.Id = Guid.NewGuid().ToString();
        variant.CreatedAt = DateTime.UtcNow;
        variant.UpdatedAt = DateTime.UtcNow;
        
        product.Variants.Add(variant);
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.Products.ReplaceOneAsync(p => p.Id == productId, product);
        return Ok(variant);
    }

    [HttpPut("{productId}/variants/{variantId}")]
    public async Task<IActionResult> UpdateVariant(string productId, string variantId, ProductVariant variant)
    {
        var product = await _context.Products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        if (product == null) return NotFound();

        var existingVariant = product.Variants.FirstOrDefault(v => v.Id == variantId);
        if (existingVariant == null) return NotFound();

        variant.Id = variantId;
        variant.CreatedAt = existingVariant.CreatedAt;
        variant.UpdatedAt = DateTime.UtcNow;
        
        var index = product.Variants.FindIndex(v => v.Id == variantId);
        product.Variants[index] = variant;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.Products.ReplaceOneAsync(p => p.Id == productId, product);
        return NoContent();
    }

    [HttpDelete("{productId}/variants/{variantId}")]
    public async Task<IActionResult> DeleteVariant(string productId, string variantId)
    {
        var product = await _context.Products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        if (product == null) return NotFound();

        product.Variants.RemoveAll(v => v.Id == variantId);
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.Products.ReplaceOneAsync(p => p.Id == productId, product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _context.Products.DeleteOneAsync(p => p.Id == id);
        return NoContent();
    }
}
