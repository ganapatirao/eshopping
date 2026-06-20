using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdvertisementsController : ControllerBase
{
    private readonly MongoDbContext _context;

    public AdvertisementsController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Advertisement>>> GetAdvertisements([FromQuery] AdPosition? position = null, [FromQuery] string? categoryId = null)
    {
        var filter = Builders<Advertisement>.Filter.And(
            Builders<Advertisement>.Filter.Eq(a => a.IsActive, true),
            Builders<Advertisement>.Filter.Lte(a => a.StartDate, DateTime.UtcNow),
            Builders<Advertisement>.Filter.Gte(a => a.EndDate, DateTime.UtcNow)
        );

        if (position.HasValue)
        {
            filter = filter & Builders<Advertisement>.Filter.Eq(a => a.Position, position.Value);
        }

        if (!string.IsNullOrEmpty(categoryId))
        {
            filter = filter & (Builders<Advertisement>.Filter.Eq(a => a.CategoryId, categoryId) | 
                              Builders<Advertisement>.Filter.Where(a => a.CategoryId == null));
        }

        var ads = await _context.Advertisements
            .Find(filter)
            .SortBy(a => a.DisplayOrder)
            .ToListAsync();
        return Ok(ads);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Advertisement>> GetAdvertisement(string id)
    {
        var ad = await _context.Advertisements.Find(a => a.Id == id).FirstOrDefaultAsync();
        if (ad == null) return NotFound();
        return Ok(ad);
    }
}
