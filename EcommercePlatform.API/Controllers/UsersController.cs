using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly MongoDbContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(MongoDbContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        try
        {
            var users = await _context.Users.Find(_ => true).ToListAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching users");
            return StatusCode(500, $"Error fetching users: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        try
        {
            var user = await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user with ID: {Id}", id);
            return StatusCode(500, $"Error fetching user: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        try
        {
            user.Id = string.Empty; // Let MongoDB generate the ID
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            
            await _context.Users.InsertOneAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(500, $"Error creating user: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(string id, User user)
    {
        try
        {
            var existingUser = await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                return NotFound();
            }

            user.Id = id;
            user.UpdatedAt = DateTime.UtcNow;
            user.CreatedAt = existingUser.CreatedAt;

            await _context.Users.ReplaceOneAsync(u => u.Id == id, user);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user with ID: {Id}", id);
            return StatusCode(500, $"Error updating user: {ex.Message}");
        }
    }

    [HttpPatch("{id}/toggle-active")]
    public async Task<ActionResult> ToggleUserActive(string id)
    {
        try
        {
            var user = await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = !user.IsActive;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.Users.ReplaceOneAsync(u => u.Id == id, user);
            return Ok(new { isActive = user.IsActive });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error toggling user active status with ID: {Id}", id);
            return StatusCode(500, $"Error toggling user active status: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        try
        {
            var result = await _context.Users.DeleteOneAsync(u => u.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user with ID: {Id}", id);
            return StatusCode(500, $"Error deleting user: {ex.Message}");
        }
    }
}
