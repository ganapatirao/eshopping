using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressConfigurationController : ControllerBase
{
    private readonly MongoDbContext _context;

    public AddressConfigurationController(MongoDbContext context)
    {
        _context = context;
    }

    // GET api/addressconfiguration
    [HttpGet]
    public async Task<ActionResult<AddressConfiguration>> GetConfiguration()
    {
        var config = await _context.AddressConfigurations
            .Find(_ => true)
            .FirstOrDefaultAsync();
        
        if (config == null)
        {
            // Create default configuration if none exists
            config = new AddressConfiguration
            {
                MaxAddressesPerType = 3,
                MaxTotalAddresses = 10
            };
            await _context.AddressConfigurations.InsertOneAsync(config);
        }
        
        return Ok(config);
    }

    // PUT api/addressconfiguration/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConfiguration(string id, [FromBody] AddressConfiguration config)
    {
        var existing = await _context.AddressConfigurations
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null)
            return NotFound();

        config.UpdatedAt = DateTime.UtcNow;
        config.Id = id;

        var result = await _context.AddressConfigurations.ReplaceOneAsync(
            c => c.Id == id, config);

        if (result.MatchedCount == 0)
            return NotFound();

        return NoContent();
    }
}
