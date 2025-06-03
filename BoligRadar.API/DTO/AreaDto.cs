namespace BoligRadar.API.DTO;

public class AreaDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Municipality { get; set; } = string.Empty;
    public int AveragePrice { get; set; }
    public int PropertyCount { get; set; }
    public DateTime LastUpdated { get; set; }
}