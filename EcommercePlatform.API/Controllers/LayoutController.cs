using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LayoutController : ControllerBase
{
    private readonly MongoDbContext _context;

    public LayoutController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet("header")]
    public async Task<ActionResult<Header>> GetHeader()
    {
        var header = await _context.Headers.Find(_ => true).FirstOrDefaultAsync();
        if (header == null) return NotFound();
        return Ok(header);
    }

    [HttpGet("footer")]
    public async Task<ActionResult<Footer>> GetFooter()
    {
        var footer = await _context.Footers.Find(_ => true).FirstOrDefaultAsync();
        if (footer == null) return NotFound();
        return Ok(footer);
    }

    [HttpPost("header")]
    public async Task<ActionResult<Header>> UpdateHeader(Header header)
    {
        var existing = await _context.Headers.Find(_ => true).FirstOrDefaultAsync();
        if (existing != null)
        {
            header.Id = existing.Id;
            header.UpdatedAt = DateTime.UtcNow;
            await _context.Headers.ReplaceOneAsync(h => h.Id == existing.Id, header);
        }
        else
        {
            header.Id = Guid.NewGuid().ToString();
            header.CreatedAt = DateTime.UtcNow;
            header.UpdatedAt = DateTime.UtcNow;
            await _context.Headers.InsertOneAsync(header);
        }
        return Ok(header);
    }

    [HttpPost("footer")]
    public async Task<ActionResult<Footer>> UpdateFooter(Footer footer)
    {
        var existing = await _context.Footers.Find(_ => true).FirstOrDefaultAsync();
        if (existing != null)
        {
            footer.Id = existing.Id;
            footer.UpdatedAt = DateTime.UtcNow;
            await _context.Footers.ReplaceOneAsync(f => f.Id == existing.Id, footer);
        }
        else
        {
            footer.Id = Guid.NewGuid().ToString();
            footer.CreatedAt = DateTime.UtcNow;
            footer.UpdatedAt = DateTime.UtcNow;
            await _context.Footers.InsertOneAsync(footer);
        }
        return Ok(footer);
    }
}
