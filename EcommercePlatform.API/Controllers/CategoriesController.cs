using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly MongoDbContext _context;

    public CategoriesController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _context.Categories
            .Find(c => c.IsActive)
            .SortBy(c => c.DisplayOrder)
            .ToListAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(string id)
    {
        var category = await _context.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpGet("root")]
    public async Task<ActionResult<IEnumerable<Category>>> GetRootCategories()
    {
        var categories = await _context.Categories
            .Find(c => c.IsActive && c.ParentCategoryId == null)
            .SortBy(c => c.DisplayOrder)
            .ToListAsync();
        return Ok(categories);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        var categories = await _context.Categories
            .Find(FilterDefinition<Category>.Empty)
            .SortBy(c => c.DisplayOrder)
            .ToListAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        if (string.IsNullOrEmpty(category.Id))
            category.Id = Guid.NewGuid().ToString();
        category.CreatedAt = DateTime.UtcNow;
        category.UpdatedAt = DateTime.UtcNow;
        await _context.Categories.InsertOneAsync(category);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(string id, Category category)
    {
        var existing = await _context.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (existing == null) return NotFound();

        category.Id = id;
        category.CreatedAt = existing.CreatedAt;
        category.UpdatedAt = DateTime.UtcNow;
        await _context.Categories.ReplaceOneAsync(c => c.Id == id, category);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _context.Categories.DeleteOneAsync(c => c.Id == id);
        return NoContent();
    }
}
