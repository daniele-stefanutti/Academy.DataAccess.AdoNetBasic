namespace AdoNetBasic.Models;

public sealed class Flight
{
    public string FlightNo { get; set; } = null!;
    public string FlightDepartTo { get; set; } = null!;
    public string FlightArriveFrom { get; set; } = null!;
    public int Distance { get; set; }
    public Airport FlightArriveFromNavigation { get; set; } = null!;
    public Airport FlightDepartToNavigation { get; set; } = null!;
    public ICollection<FlightInstance> FlightInstances { get; set; } = new List<FlightInstance>();
}
