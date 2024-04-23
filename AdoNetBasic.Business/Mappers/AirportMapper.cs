using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Models;

namespace AdoNetBasic.Business.Mappers;

internal static class AirportMapper
{
    /// <remarks>
    /// Example
    /// </remarks>
    public static AirportDto Map(Airport airport, string countryName)
        => new
        (
            Code: airport.AirportCode,
            Longitude: airport.Longitude,
            Latitude: airport.Latitude,
            CountryName: countryName
        );
}
