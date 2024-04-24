namespace AdoNetBasic.Models;

public sealed class FlightInstance
{
    public int InstanceId { get; set; }
    public string FlightNo { get; set; } = null!;
    public int PlaneId { get; set; }
    public int PilotAboardId { get; set; }
    public int CoPilotAboardId { get; set; }
    public int Fsm_AttendantId { get; set; }
    public DateTime DateTimeLeave { get; set; }
    public DateTime DateTimeArrive { get; set; }
}
