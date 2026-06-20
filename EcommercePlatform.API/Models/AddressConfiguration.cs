using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace EcommercePlatform.API.Models;

public class AddressConfiguration
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [BsonElement("maxAddressesPerType")]
    [JsonPropertyName("maxAddressesPerType")]
    public int MaxAddressesPerType { get; set; } = 3;

    [BsonElement("maxTotalAddresses")]
    [JsonPropertyName("maxTotalAddresses")]
    public int MaxTotalAddresses { get; set; } = 10;

    [BsonElement("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; } = "Address configuration limits";

    [BsonElement("createdAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
