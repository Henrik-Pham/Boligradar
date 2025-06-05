namespace BoligRadar.API.DTO;

public class PropertyDto
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Size { get; set; }
    public int Bedrooms { get; set; }
    public string PropertyType { get; set; } = string.Empty;
    public DateTime ListedAt { get; set; }
    public string AreaName { get; set; } = string.Empty;
    public string Postcode { get; set; } = string.Empty;
    public int PricePerSquareMeter => Size > 0 ? Price / Size : 0;

}