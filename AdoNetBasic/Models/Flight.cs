namespace AdoNetBasic.Models;

public sealed class Flight
{
    public string FlightNo { get; set; } = null!;
    public string FlightDepartTo { get; set; } = null!;
    public string FlightArriveFrom { get; set; } = null!;
    public int Distance { get; set; }
}
