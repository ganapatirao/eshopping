using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SavedAddressesController : ControllerBase
{
    private readonly MongoDbContext _context;

    public SavedAddressesController(MongoDbContext context)
    {
        _context = context;
    }

    // GET api/savedaddresses/{userId}
    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<SavedAddress>>> GetByUserId(string userId)
    {
        var addresses = await _context.SavedAddresses
            .Find(a => a.UserId == userId)
            .SortByDescending(a => a.IsDefault)
            .ThenByDescending(a => a.UpdatedAt)
            .ToListAsync();
        return Ok(addresses);
    }

    // GET api/savedaddresses/{userId}/default
    [HttpGet("{userId}/default")]
    public async Task<ActionResult<SavedAddress>> GetDefaultAddress(string userId)
    {
        var address = await _context.SavedAddresses
            .Find(a => a.UserId == userId && a.IsDefault)
            .FirstOrDefaultAsync();
        
        if (address == null)
        {
            // If no default, return the most recently created address
            address = await _context.SavedAddresses
                .Find(a => a.UserId == userId)
                .SortByDescending(a => a.CreatedAt)
                .FirstOrDefaultAsync();
        }
        
        return address == null ? NotFound() : Ok(address);
    }

    // GET api/savedaddresses/single/{id}
    [HttpGet("single/{id}")]
    public async Task<ActionResult<SavedAddress>> GetById(string id)
    {
        var address = await _context.SavedAddresses
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync();
        
        return address == null ? NotFound() : Ok(address);
    }

    // POST api/savedaddresses
    [HttpPost]
    public async Task<ActionResult<SavedAddress>> Create([FromBody] SavedAddress address)
    {
        try
        {
            Console.WriteLine($"Creating saved address for user: {address.UserId}");
            Console.WriteLine($"Address data: {System.Text.Json.JsonSerializer.Serialize(address)}");

            // Get address configuration
            var config = await _context.AddressConfigurations
                .Find(_ => true)
                .FirstOrDefaultAsync();
            
            if (config == null)
            {
                config = new AddressConfiguration { MaxAddressesPerType = 3, MaxTotalAddresses = 10 };
            }

            // Check if user has reached the limit for this address type
            var typeCount = await _context.SavedAddresses
                .CountDocumentsAsync(a => a.UserId == address.UserId && a.AddressType == address.AddressType);
            
            if (typeCount >= config.MaxAddressesPerType)
            {
                return BadRequest(new { 
                    error = $"Maximum of {config.MaxAddressesPerType} addresses allowed for {address.AddressType} type" 
                });
            }

            // Check if user has reached total address limit
            var totalCount = await _context.SavedAddresses
                .CountDocumentsAsync(a => a.UserId == address.UserId);
            
            if (totalCount >= config.MaxTotalAddresses)
            {
                return BadRequest(new { 
                    error = $"Maximum of {config.MaxTotalAddresses} total addresses allowed" 
                });
            }

            address.Id = string.Empty;
            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;

            // If this is set as default, unset other defaults for this user
            if (address.IsDefault)
            {
                var filter = Builders<SavedAddress>.Filter.Eq(a => a.UserId, address.UserId);
                var update = Builders<SavedAddress>.Update.Set(a => a.IsDefault, false);
                await _context.SavedAddresses.UpdateManyAsync(filter, update);
            }

            await _context.SavedAddresses.InsertOneAsync(address);
            Console.WriteLine($"Address saved successfully with ID: {address.Id}");
            return CreatedAtAction(nameof(GetById), new { id = address.Id }, address);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating saved address: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    // PUT api/savedaddresses/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] SavedAddress address)
    {
        var existing = await _context.SavedAddresses
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null)
            return NotFound();

        // If setting as default, unset other defaults for this user
        if (address.IsDefault && !existing.IsDefault)
        {
            var filter = Builders<SavedAddress>.Filter.Eq(a => a.UserId, address.UserId);
            var update = Builders<SavedAddress>.Update.Set(a => a.IsDefault, false);
            await _context.SavedAddresses.UpdateManyAsync(filter, update);
        }

        address.UpdatedAt = DateTime.UtcNow;
        address.Id = id;
        address.UserId = existing.UserId; // Preserve original userId
        address.CreatedAt = existing.CreatedAt; // Preserve original createdAt

        var result = await _context.SavedAddresses.ReplaceOneAsync(
            a => a.Id == id, address);

        if (result.MatchedCount == 0)
            return NotFound();

        return NoContent();
    }

    // PUT api/savedaddresses/{id}/setdefault
    [HttpPut("{id}/setdefault")]
    public async Task<IActionResult> SetDefault(string id)
    {
        var address = await _context.SavedAddresses
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync();

        if (address == null)
            return NotFound();

        // Unset all other defaults for this user
        var filter = Builders<SavedAddress>.Filter.Eq(a => a.UserId, address.UserId);
        var update = Builders<SavedAddress>.Update.Set(a => a.IsDefault, false);
        await _context.SavedAddresses.UpdateManyAsync(filter, update);

        // Set this address as default
        var addressFilter = Builders<SavedAddress>.Filter.Eq(a => a.Id, id);
        var addressUpdate = Builders<SavedAddress>.Update
            .Set(a => a.IsDefault, true)
            .Set(a => a.UpdatedAt, DateTime.UtcNow);
        await _context.SavedAddresses.UpdateOneAsync(addressFilter, addressUpdate);

        return NoContent();
    }

    // DELETE api/savedaddresses/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _context.SavedAddresses.DeleteOneAsync(a => a.Id == id);

        if (result.DeletedCount == 0)
            return NotFound();

        return NoContent();
    }
}
