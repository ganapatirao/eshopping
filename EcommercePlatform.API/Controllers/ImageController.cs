using EcommercePlatform.API.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

namespace EcommercePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly MongoDbContext _context;
    private readonly ILogger<ImageController> _logger;

    public ImageController(MongoDbContext context, ILogger<ImageController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<string>> UploadImage(IFormFile file)
    {
        _logger.LogInformation("UploadImage called with file: {FileName}, ContentType: {ContentType}, Length: {Length}", 
            file?.FileName, file?.ContentType, file?.Length);
        
        if (file == null || file.Length == 0)
        {
            _logger.LogWarning("No file uploaded");
            return BadRequest("No file uploaded");
        }

        try
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var imageDoc = new BsonDocument
            {
                { "fileName", file.FileName },
                { "contentType", file.ContentType },
                { "data", imageBytes },
                { "uploadedAt", DateTime.UtcNow }
            };

            await _context.Images.InsertOneAsync(imageDoc);
            
            var imageId = imageDoc["_id"].ToString();
            _logger.LogInformation("Image uploaded successfully with ID: {ImageId}", imageId);
            
            return Ok(imageId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading image");
            return StatusCode(500, $"Error uploading image: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetImage(string id)
    {
        _logger.LogInformation("GetImage called with ID: {Id}", id);
        
        try
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var image = await _context.Images.Find(filter).FirstOrDefaultAsync();

            if (image == null)
            {
                _logger.LogWarning("Image not found with ID: {Id}", id);
                return NotFound();
            }

            var contentType = image["contentType"].AsString;
            var data = image["data"].AsByteArray;

            _logger.LogInformation("Image retrieved successfully with ID: {Id}, ContentType: {ContentType}", id, contentType);
            
            return File(data, contentType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving image with ID: {Id}", id);
            return StatusCode(500, $"Error retrieving image: {ex.Message}");
        }
    }
}
