namespace AdoNetBasic.Models;

public sealed class PlaneModel
{
    public string ModelNumber { get; set; } = null!;
    public string ManufacturerName { get; set; } = null!;
    public short PlaneRange { get; set; }
    public short CruiseSpeed { get; set; }
}
