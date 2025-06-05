namespace BoligRadar.API.DTO;

public class CreateSearchDto
{
    public string Name { get; set; } = string.Empty;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinSize { get; set; }
    public int? MaxSize { get; set; }
    public string? PropertyType { get; set; }
    public List<string> PostalCodes { get; set; } = new();
    public bool EmailNotifications { get; set; }
}