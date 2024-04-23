using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Models;

namespace AdoNetBasic.Business.Mappers;

internal static class FlightMapper
{
    /// <remarks>
    /// Example
    /// </remarks>
    public static FlightDto Map(Flight flight, AirportDto departureAirport, AirportDto arrivalAirport)
        => new
        (
            FlightNo: flight.FlightNo,
            DepartureAirport: departureAirport,
            ArrivalAirport: arrivalAirport,
            Distance: flight.Distance
        );
}
