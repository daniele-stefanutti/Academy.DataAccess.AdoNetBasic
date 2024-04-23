using AdoNetBasic.Business.Dtos;

namespace AdoNetBasic.Business.Services;

public interface IFlightService
{
    Task<FlightDto?> GetFlightWithLongestDistanceAsync();
    Task<IReadOnlyList<FlightDto>> GetAllFlightsDepartingFromCountryAsync(string countryCode);
}
