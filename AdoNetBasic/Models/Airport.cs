namespace AdoNetBasic.Models;

public sealed class Airport
{
    public string AirportCode { get; set; } = null!;
    public string AirportName { get; set; } = null!;
    public decimal ContactNo { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string CountryCode { get; set; } = null!;
    public Country CountryCodeNavigation { get; set; } = null!;
    public ICollection<Flight> FlightFlightArriveFromNavigations { get; set; } = new List<Flight>();
    public ICollection<Flight> FlightFlightDepartToNavigations { get; set; } = new List<Flight>();
}
