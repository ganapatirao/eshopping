namespace EcommercePlatform.API.Models;

public class FooterLink
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string? Link { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class FooterSection
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<FooterLink> Links { get; set; } = new();
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Footer
{
    public string Id { get; set; } = string.Empty;
    public string? CompanyName { get; set; }
    public string? Description { get; set; }
    public List<FooterSection> Sections { get; set; } = new();
    public string? CopyrightText { get; set; }
    public List<string> SocialLinks { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
