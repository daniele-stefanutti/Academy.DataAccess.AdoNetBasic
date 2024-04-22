namespace AdoNetBasic.Models;

public sealed class PlaneModel
{
    public string ModelNumber { get; set; } = null!;
    public string ManufacturerName { get; set; } = null!;
    public short PlaneRange { get; set; }
    public short CruiseSpeed { get; set; }
    public ICollection<PlaneDetail> PlaneDetails { get; set; } = new List<PlaneDetail>();
    public ICollection<Pilot> Pilots { get; set; } = new List<Pilot>();
}
