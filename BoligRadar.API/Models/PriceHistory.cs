namespace BoligRadar.API.Models;

public class PriceHistory
{
    public int Id { get; set; }
    public int PostalCode { get; set; }
    public int AveragePrice { get; set; }
    public int PropertyCount { get; set; }
    public DateTime RecordedAt { get; set; }

    public Area Area { get; set; } = null!;
}