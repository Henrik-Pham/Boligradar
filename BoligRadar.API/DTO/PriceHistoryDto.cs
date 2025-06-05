namespace BoligRadar.API.DTO;

public class PriceHistoryDto
{
    public DateTime Date { get; set; }
    public decimal AveragePrice { get; set; }
    public int PropertyCount { get; set; }
}