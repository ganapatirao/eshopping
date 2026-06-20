namespace EcommercePlatform.API.Models;

public class ValidationSetting
{
    public string Id { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public ValidationRules ValidationRules { get; set; } = new();
    public ErrorMessages ErrorMessages { get; set; } = new();
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class ValidationRules
{
    public bool Required { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public string? RegexPattern { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public List<string>? AllowedValues { get; set; }
}

public class ErrorMessages
{
    public string? Required { get; set; }
    public string? MinLength { get; set; }
    public string? MaxLength { get; set; }
    public string? Pattern { get; set; }
    public string? MinValue { get; set; }
    public string? MaxValue { get; set; }
    public string? InvalidValue { get; set; }
}
