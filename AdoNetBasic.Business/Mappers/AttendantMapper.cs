using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Models;

namespace AdoNetBasic.Business.Mappers;

internal static class AttendantMapper
{
    public static AttendantDto Map(FlightAttendant attendant, bool isMentor)
        => new
        (
            FirstName: attendant.FirstName,
            LastName: attendant.LastName,
            IsMentor: isMentor
        );
}
