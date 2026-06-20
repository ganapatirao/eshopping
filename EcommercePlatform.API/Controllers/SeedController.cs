using EcommercePlatform.API.Seed;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly SeedData _seedData;

    public SeedController(SeedData seedData)
    {
        _seedData = seedData;
    }

    [HttpPost]
    public async Task<IActionResult> SeedDatabase()
    {
        await _seedData.SeedAsync();
        return Ok(new { message = "Database seeded successfully" });
    }
}
