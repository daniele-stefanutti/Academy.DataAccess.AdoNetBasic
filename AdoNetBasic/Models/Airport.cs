namespace AdoNetBasic.Models;

public sealed class Airport
{
    public string AirportCode { get; set; } = null!;
    public string AirportName { get; set; } = null!;
    public decimal ContactNo { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string CountryCode { get; set; } = null!;
}
