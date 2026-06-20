using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly MongoDbContext _context;

    public CartController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems(string userId)
    {
        var items = await _context.CartItems
            .Find(c => c.UserId == userId)
            .ToListAsync();
        return Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult<CartItem>> AddToCart(CartItem item)
    {
        item.Id = Guid.NewGuid().ToString();
        item.CreatedAt = DateTime.UtcNow;
        item.UpdatedAt = DateTime.UtcNow;
        await _context.CartItems.InsertOneAsync(item);
        return CreatedAtAction(nameof(GetCartItems), new { userId = item.UserId }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(string id, CartItem item)
    {
        var existing = await _context.CartItems.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (existing == null) return NotFound();

        item.UpdatedAt = DateTime.UtcNow;
        await _context.CartItems.ReplaceOneAsync(c => c.Id == id, item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartItem(string id)
    {
        await _context.CartItems.DeleteOneAsync(c => c.Id == id);
        return NoContent();
    }

    [HttpDelete("user/{userId}")]
    public async Task<IActionResult> ClearCart(string userId)
    {
        await _context.CartItems.DeleteManyAsync(c => c.UserId == userId);
        return NoContent();
    }
}
