namespace AdoNetBasic.Models;

public sealed class Pilot
{
    public int PilotId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Dob { get; set; }
    public short HoursFlown { get; set; }
}
