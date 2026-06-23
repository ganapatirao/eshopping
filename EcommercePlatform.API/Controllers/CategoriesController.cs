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

    // Subcategory endpoints
    [HttpGet("subcategories")]
    public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories()
    {
        var subCategories = await _context.SubCategories
            .Find(sc => sc.IsActive)
            .SortBy(sc => sc.DisplayOrder)
            .ToListAsync();
        return Ok(subCategories);
    }

    [HttpGet("subcategories/{id}")]
    public async Task<ActionResult<SubCategory>> GetSubCategory(string id)
    {
        var subCategory = await _context.SubCategories.Find(sc => sc.Id == id).FirstOrDefaultAsync();
        if (subCategory == null) return NotFound();
        return Ok(subCategory);
    }

    [HttpGet("subcategories/by-category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategoriesByCategory(string categoryId)
    {
        var subCategories = await _context.SubCategories
            .Find(sc => sc.IsActive && sc.ParentCategoryId == categoryId)
            .SortBy(sc => sc.DisplayOrder)
            .ToListAsync();
        return Ok(subCategories);
    }

    [HttpPost("subcategories")]
    public async Task<ActionResult<SubCategory>> CreateSubCategory(SubCategory subCategory)
    {
        if (string.IsNullOrEmpty(subCategory.Id))
            subCategory.Id = Guid.NewGuid().ToString();
        subCategory.CreatedAt = DateTime.UtcNow;
        subCategory.UpdatedAt = DateTime.UtcNow;
        await _context.SubCategories.InsertOneAsync(subCategory);
        return CreatedAtAction(nameof(GetSubCategory), new { id = subCategory.Id }, subCategory);
    }

    [HttpPut("subcategories/{id}")]
    public async Task<IActionResult> UpdateSubCategory(string id, SubCategory subCategory)
    {
        var existing = await _context.SubCategories.Find(sc => sc.Id == id).FirstOrDefaultAsync();
        if (existing == null) return NotFound();

        subCategory.Id = id;
        subCategory.CreatedAt = existing.CreatedAt;
        subCategory.UpdatedAt = DateTime.UtcNow;
        await _context.SubCategories.ReplaceOneAsync(sc => sc.Id == id, subCategory);
        return NoContent();
    }

    [HttpDelete("subcategories/{id}")]
    public async Task<IActionResult> DeleteSubCategory(string id)
    {
        await _context.SubCategories.DeleteOneAsync(sc => sc.Id == id);
        return NoContent();
    }
}
