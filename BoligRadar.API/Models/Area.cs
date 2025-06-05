namespace BoligRadar.API.Models;

public class Area
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Municipality { get; set; } = string.Empty;
    public decimal AveragePrice { get; set; }
    public DateTime LastUpdated { get; set; }
    
    public List<Property> Properties { get; set; } = new();
    public List<PriceHistory> PriceHistories { get; set; } = new();
    
}