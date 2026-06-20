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
}
