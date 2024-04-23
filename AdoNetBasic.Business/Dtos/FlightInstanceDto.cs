namespace AdoNetBasic.Business.Dtos;

public sealed record FlightInstanceDto
(
    string FlightNo,
    string DepartTo,
    string ArriveFrom,
    DateTime DateTimeLeave,
    DateTime DateTimeArrive,
    PlaneDto Plane,
    PilotDto Pilot,
    IReadOnlyList<AttendantDto> AllAttendants
);
