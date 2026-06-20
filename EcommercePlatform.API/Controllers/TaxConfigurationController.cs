using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxConfigurationController : ControllerBase
{
    private readonly MongoDbContext _context;

    public TaxConfigurationController(MongoDbContext context)
    {
        _context = context;
    }

    // GET api/taxconfiguration
    [HttpGet]
    public async Task<ActionResult<TaxConfiguration>> GetActiveTaxConfiguration()
    {
        var config = await _context.TaxConfigurations
            .Find(c => c.IsActive)
            .FirstOrDefaultAsync();
        
        if (config == null)
        {
            // Return default configuration if none exists
            return Ok(new TaxConfiguration
            {
                TaxRate = 0.10m,
                TaxName = "GST",
                Description = "Goods and Services Tax",
                IsActive = true,
                FreeShippingThreshold = 100m,
                ShippingCost = 10m
            });
        }
        
        return Ok(config);
    }

    // PUT api/taxconfiguration/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] TaxConfiguration config)
    {
        var existing = await _context.TaxConfigurations
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null)
            return NotFound();

        config.UpdatedAt = DateTime.UtcNow;
        config.Id = id;

        var result = await _context.TaxConfigurations.ReplaceOneAsync(
            c => c.Id == id, config);

        if (result.MatchedCount == 0)
            return NotFound();

        return NoContent();
    }

    // POST api/taxconfiguration
    [HttpPost]
    public async Task<ActionResult<TaxConfiguration>> Create([FromBody] TaxConfiguration config)
    {
        config.Id = string.Empty;
        config.CreatedAt = DateTime.UtcNow;
        config.UpdatedAt = DateTime.UtcNow;

        await _context.TaxConfigurations.InsertOneAsync(config);
        return CreatedAtAction(nameof(GetActiveTaxConfiguration), new { id = config.Id }, config);
    }
}
