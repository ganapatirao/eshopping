using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using EcommercePlatform.API.Seed;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValidationController : ControllerBase
{
    private readonly MongoDbContext _context;

    public ValidationController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet("settings/{entityType}")]
    public async Task<ActionResult<Dictionary<string, ValidationSetting>>> GetSettings(string entityType)
    {
        var settings = await _context.ValidationSettings
            .Find(v => v.EntityType == entityType && v.IsActive)
            .ToListAsync();

        var dict = settings
            .GroupBy(s => s.FieldName)
            .ToDictionary(g => g.Key, g => g.First());

        return Ok(dict);
    }

    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        var existing = await _context.ValidationSettings.CountDocumentsAsync(FilterDefinition<ValidationSetting>.Empty);
        if (existing == 0)
        {
            await _context.ValidationSettings.InsertManyAsync(ValidationSeedData.GetAllSettings());
        }
        var count = await _context.ValidationSettings.CountDocumentsAsync(FilterDefinition<ValidationSetting>.Empty);
        return Ok(new { message = "Validation settings ready", count });
    }

    [HttpPost("reseed")]
    public async Task<IActionResult> Reseed()
    {
        await _context.ValidationSettings.DeleteManyAsync(FilterDefinition<ValidationSetting>.Empty);
        await _context.ValidationSettings.InsertManyAsync(ValidationSeedData.GetAllSettings());
        var count = await _context.ValidationSettings.CountDocumentsAsync(FilterDefinition<ValidationSetting>.Empty);
        return Ok(new { message = "Validation settings reseeded", count });
    }
}
