namespace EcommercePlatform.API.Models;

public class ColorOption
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Hex code for color swatch
    public List<string> Images { get; set; } = new(); // Base64 encoded images for this color
}
