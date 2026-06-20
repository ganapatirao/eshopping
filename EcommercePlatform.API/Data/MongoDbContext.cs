using EcommercePlatform.API.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace EcommercePlatform.API.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB") ?? "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("EcommercePlatform");
    }

    public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    public IMongoCollection<CartItem> CartItems => _database.GetCollection<CartItem>("CartItems");
    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    public IMongoCollection<ValidationSetting> ValidationSettings => _database.GetCollection<ValidationSetting>("ValidationSettings");
    public IMongoCollection<Header> Headers => _database.GetCollection<Header>("Headers");
    public IMongoCollection<Footer> Footers => _database.GetCollection<Footer>("Footers");
    public IMongoCollection<Advertisement> Advertisements => _database.GetCollection<Advertisement>("Advertisements");
    public IMongoCollection<SavedAddress> SavedAddresses => _database.GetCollection<SavedAddress>("SavedAddresses");
    public IMongoCollection<TaxConfiguration> TaxConfigurations => _database.GetCollection<TaxConfiguration>("TaxConfigurations");
    public IMongoCollection<AddressConfiguration> AddressConfigurations => _database.GetCollection<AddressConfiguration>("AddressConfigurations");
    public IMongoCollection<BsonDocument> Images => _database.GetCollection<BsonDocument>("Images");
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}
