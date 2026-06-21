using EcommercePlatform.API.Data;
using EcommercePlatform.API.Seed;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly SeedData _seedData;
    private readonly MongoDbContext _context;

    public SeedController(SeedData seedData, MongoDbContext context)
    {
        _seedData = seedData;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SeedDatabase()
    {
        await _seedData.SeedAsync();
        return Ok(new { message = "Database seeded successfully" });
    }

    [HttpPost("cleanup-footer")]
    public async Task<IActionResult> CleanupFooter()
    {
        await _context.Footers.DeleteManyAsync(FilterDefinition<Models.Footer>.Empty);
        return Ok(new { message = "Footer collection cleaned successfully" });
    }

    [HttpPost("cleanup-products")]
    public async Task<IActionResult> CleanupProducts()
    {
        await _context.Products.DeleteManyAsync(FilterDefinition<Models.Product>.Empty);
        return Ok(new { message = "Products collection cleaned successfully" });
    }

    [HttpPost("cleanup-all")]
    public async Task<IActionResult> CleanupAll()
    {
        await _context.Products.DeleteManyAsync(FilterDefinition<Models.Product>.Empty);
        await _context.Categories.DeleteManyAsync(FilterDefinition<Models.Category>.Empty);
        await _context.Users.DeleteManyAsync(FilterDefinition<Models.User>.Empty);
        await _context.Headers.DeleteManyAsync(FilterDefinition<Models.Header>.Empty);
        await _context.Footers.DeleteManyAsync(FilterDefinition<Models.Footer>.Empty);
        await _context.Advertisements.DeleteManyAsync(FilterDefinition<Models.Advertisement>.Empty);
        return Ok(new { message = "All collections cleaned successfully" });
    }
}
