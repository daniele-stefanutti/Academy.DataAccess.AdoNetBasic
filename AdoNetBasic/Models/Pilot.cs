namespace AdoNetBasic.Models;

public sealed class Pilot
{
    public int PilotId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Dob { get; set; }
    public short HoursFlown { get; set; }
    public ICollection<FlightInstance> FlightInstanceCoPilotAboards { get; set; } = new List<FlightInstance>();
    public ICollection<FlightInstance> FlightInstancePilotAboards { get; set; } = new List<FlightInstance>();
    public ICollection<PlaneModel> PlaneModels { get; set; } = new List<PlaneModel>();
}
