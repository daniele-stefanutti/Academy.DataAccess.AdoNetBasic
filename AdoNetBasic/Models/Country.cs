namespace AdoNetBasic.Models;

public sealed class Country
{
    public string CountryName { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
    public ICollection<Airport> Airports { get; set; } = new List<Airport>();
}
