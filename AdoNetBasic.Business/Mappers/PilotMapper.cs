using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Models;

namespace AdoNetBasic.Business.Mappers;

internal static class PilotMapper
{
    public static PilotDto Map(Pilot pilot, int age)
        => new
        (
            FirstName: pilot.FirstName,
            LastName: pilot.LastName,
            Age: age
        );
}
