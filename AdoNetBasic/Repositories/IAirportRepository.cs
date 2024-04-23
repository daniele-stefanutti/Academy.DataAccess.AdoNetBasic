using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IAirportRepository
{
    Airport? GetByAirportCode(string airportCode);
    IReadOnlyList<Airport> GetByCountryCode(string countryCode);
    IReadOnlyList<Airport> GetBySquareArea(double northWestLongitude, double northWestLatitude, double southEastLongitude, double southEastLatitude);
    int Add(Airport airport);
    int AddRange(IReadOnlyList<Airport> airports);
    int Update(Airport airport);
    int DeleteByAirportCode(string airportCode);
    int DeleteByCountryCode(string countryCode);
}
