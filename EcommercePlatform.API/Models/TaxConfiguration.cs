using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace EcommercePlatform.API.Models;

public class TaxConfiguration
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [BsonElement("taxRate")]
    [JsonPropertyName("taxRate")]
    public decimal TaxRate { get; set; } = 0.10m; // Default 10%

    [BsonElement("taxName")]
    [JsonPropertyName("taxName")]
    public string TaxName { get; set; } = "GST";

    [BsonElement("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; } = "Goods and Services Tax";

    [BsonElement("isActive")]
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;

    [BsonElement("freeShippingThreshold")]
    [JsonPropertyName("freeShippingThreshold")]
    public decimal FreeShippingThreshold { get; set; } = 100m;

    [BsonElement("shippingCost")]
    [JsonPropertyName("shippingCost")]
    public decimal ShippingCost { get; set; } = 10m;

    [BsonElement("createdAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
