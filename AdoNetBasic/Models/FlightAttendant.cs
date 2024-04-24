namespace AdoNetBasic.Models;

public sealed class FlightAttendant
{
    public int AttendantId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Dob { get; set; }
    public DateTime HireDate { get; set; }
    public int? MentorId { get; set; }
}
