using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly MongoDbContext _context;

    public OrdersController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _context.Orders
            .Find(FilterDefinition<Order>.Empty)
            .SortByDescending(o => o.CreatedAt)
            .ToListAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(string id)
    {
        var order = await _context.Orders.Find(o => o.Id == id).FirstOrDefaultAsync();
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetUserOrders(string userId)
    {
        var orders = await _context.Orders
            .Find(o => o.UserId == userId)
            .SortByDescending(o => o.CreatedAt)
            .ToListAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        order.Id = Guid.NewGuid().ToString();
        order.CreatedAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;
        if (string.IsNullOrEmpty(order.Status)) order.Status = "Pending";
        await _context.Orders.InsertOneAsync(order);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    public class StatusUpdate
    {
        public string Status { get; set; } = string.Empty;
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(string id, StatusUpdate update)
    {
        var existing = await _context.Orders.Find(o => o.Id == id).FirstOrDefaultAsync();
        if (existing == null) return NotFound();

        var filter = Builders<Order>.Filter.Eq(o => o.Id, id);
        var updateDef = Builders<Order>.Update
            .Set(o => o.Status, update.Status)
            .Set(o => o.UpdatedAt, DateTime.UtcNow);
        await _context.Orders.UpdateOneAsync(filter, updateDef);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(string id)
    {
        await _context.Orders.DeleteOneAsync(o => o.Id == id);
        return NoContent();
    }
}
