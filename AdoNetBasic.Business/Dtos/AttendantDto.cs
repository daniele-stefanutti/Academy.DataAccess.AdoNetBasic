namespace AdoNetBasic.Business.Dtos;

/// <param name="IsMentor">True if attendant is referenced by one or more of the other attendants as mentor</param>
public sealed record AttendantDto
(
    string FirstName,
    string LastName,
    bool IsMentor
);
