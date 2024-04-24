using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Models;

namespace AdoNetBasic.Business.Mappers;

internal static class FlightInstanceMapper
{
    public static FlightInstanceDto Map(FlightInstance flightInstance, Flight flight, PlaneDto planeDto,
        PilotDto pilotDto, IReadOnlyList<AttendantDto> allAttendantsDtos)
        => new
        (
            FlightNo: flightInstance.FlightNo,
            DepartTo: flight.FlightDepartTo,
            ArriveFrom: flight.FlightArriveFrom,
            DateTimeLeave: flightInstance.DateTimeLeave,
            DateTimeArrive: flightInstance.DateTimeArrive,
            Plane: planeDto,
            Pilot: pilotDto,
            AllAttendants: allAttendantsDtos
        );
}
