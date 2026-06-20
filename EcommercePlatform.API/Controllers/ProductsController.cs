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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? categoryId = null)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.IsActive, true);
        
        if (!string.IsNullOrEmpty(categoryId))
        {
            filter = filter & Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        }

        var products = await _context.Products
            .Find(filter)
            .ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        var product = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet("featured")]
    public async Task<ActionResult<IEnumerable<Product>>> GetFeaturedProducts()
    {
        var products = await _context.Products
            .Find(p => p.IsActive && p.IsFeatured)
            .ToListAsync();
        return Ok(products);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryId)
    {
        var products = await _context.Products
            .Find(p => p.IsActive && p.CategoryId == categoryId)
            .ToListAsync();
        return Ok(products);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _context.Products
            .Find(FilterDefinition<Product>.Empty)
            .ToListAsync();
        return Ok(products);
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
        await _context.Products.ReplaceOneAsync(p => p.Id == id, product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _context.Products.DeleteOneAsync(p => p.Id == id);
        return NoContent();
    }
}
