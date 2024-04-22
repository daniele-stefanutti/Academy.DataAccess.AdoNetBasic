namespace AdoNetBasic.Models;

public sealed class FlightInstance
{
    public int InstanceId { get; set; }
    public string FlightNo { get; set; } = null!;
    public int PlaneId { get; set; }
    public int PilotAboardId { get; set; }
    public int CoPilotAboardId { get; set; }
    public int FsmAttendantId { get; set; }
    public DateTime DateTimeLeave { get; set; }
    public DateTime DateTimeArrive { get; set; }
    public Pilot CoPilotAboard { get; set; } = null!;
    public Flight FlightNoNavigation { get; set; } = null!;
    public FlightAttendant FsmAttendant { get; set; } = null!;
    public Pilot PilotAboard { get; set; } = null!;
    public PlaneDetail Plane { get; set; } = null!;
    public ICollection<FlightAttendant> Attendants { get; set; } = new List<FlightAttendant>();
}
