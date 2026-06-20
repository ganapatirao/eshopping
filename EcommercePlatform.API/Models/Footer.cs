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

public class SocialMediaLink
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty; // e.g., "Facebook", "Twitter", "Instagram"
    public string Url { get; set; } = string.Empty;
    public string? Icon { get; set; } // Optional icon name or URL
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class ContactInfo
{
    public string Id { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
}

public class Footer
{
    public string Id { get; set; } = string.Empty;
    public string? CompanyName { get; set; }
    public string? Description { get; set; }
    public List<FooterSection> Sections { get; set; } = new();
    public string? CopyrightText { get; set; }
    public List<SocialMediaLink> SocialLinks { get; set; } = new();
    public ContactInfo? ContactInfo { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
