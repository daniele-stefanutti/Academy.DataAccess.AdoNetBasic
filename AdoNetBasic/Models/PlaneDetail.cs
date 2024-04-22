namespace AdoNetBasic.Models;

public sealed class PlaneDetail
{
    public int PlaneId { get; set; }
    public string ModelNumber { get; set; } = null!;
    public string RegistrationNo { get; set; } = null!;
    public short BuiltYear { get; set; }
    public short FirstClassCapacity { get; set; }
    public short EcoCapacity { get; set; }
    public ICollection<FlightInstance> FlightInstances { get; set; } = new List<FlightInstance>();
    public PlaneModel ModelNumberNavigation { get; set; } = null!;
}
