namespace BoligRadar.API.Models;

public class SavedSearch
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinSize { get; set; }
    public int? MaxSize { get; set; }
    public string? PropertyType { get; set; }
    public string? Area  { get; set; }
    public bool EmailNotification { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
}