namespace EcommercePlatform.API.Models;

public class MenuItem
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string? Link { get; set; }
    public string? Icon { get; set; } // Can be base64 string or image ID
    public string? IconType { get; set; } // "base64" or "binary"
    public List<MenuItem> SubMenus { get; set; } = new();
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Header
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public List<MenuItem> MenuItems { get; set; } = new();
    public bool ShowCartIcon { get; set; } = true;
    public bool ShowUserIcon { get; set; } = true;
    public bool ShowSearchIcon { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
