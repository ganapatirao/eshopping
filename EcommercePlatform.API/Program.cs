using EcommercePlatform.API.Data;
using EcommercePlatform.API.Models;
using EcommercePlatform.API.Seed;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce Platform API", Version = "v1" });
});

// Configure MongoDB
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<SeedData>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// // Auto-seed validation settings if the collection is empty
// using (var scope = app.Services.CreateScope())
// {
//     try
//     {
//         var ctx = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
//         var existing = await ctx.ValidationSettings.CountDocumentsAsync(FilterDefinition<ValidationSetting>.Empty);
//         if (existing == 0)
//         {
//             await ctx.ValidationSettings.InsertManyAsync(ValidationSeedData.GetAllSettings());
//         }
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Validation seed skipped: {ex.Message}");
//     }
// }

app.Run();
