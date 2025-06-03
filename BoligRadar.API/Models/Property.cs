namespace BoligRadar.API.Models;

public class Property
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Size { get; set; } // Square meter
    public int Bedrooms { get; set; }
    public string PropertyType { get; set; } = string.Empty; // Apartment, Detached house etc..
    public DateTime ListedDate { get; set; }
    public string AdvertisementId { get; set; } = string.Empty; // Finn.no ID etc
    public int PostalCode { get; set; }

    public Area Area { get; set; } = null!; 
}