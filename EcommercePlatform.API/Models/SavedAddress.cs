using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace EcommercePlatform.API.Models;

public class SavedAddress
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [BsonElement("userId")]
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [BsonElement("fullName")]
    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = string.Empty;

    [BsonElement("email")]
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [BsonElement("phone")]
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [BsonElement("address")]
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [BsonElement("city")]
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [BsonElement("district")]
    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;

    [BsonElement("state")]
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [BsonElement("zipCode")]
    [JsonPropertyName("zipCode")]
    public string ZipCode { get; set; } = string.Empty;

    [BsonElement("country")]
    [JsonPropertyName("country")]
    public string Country { get; set; } = "India";

    [BsonElement("landmark")]
    [JsonPropertyName("landmark")]
    public string? Landmark { get; set; }

    [BsonElement("buildingName")]
    [JsonPropertyName("buildingName")]
    public string? BuildingName { get; set; }

    [BsonElement("floorUnit")]
    [JsonPropertyName("floorUnit")]
    public string? FloorUnit { get; set; }

    [BsonElement("alternatePhone")]
    [JsonPropertyName("alternatePhone")]
    public string? AlternatePhone { get; set; }

    [BsonElement("addressType")]
    [JsonPropertyName("addressType")]
    public string AddressType { get; set; } = "Home"; // Home, Work, Other

    [BsonElement("isDefault")]
    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; } = false;

    [BsonElement("createdAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
