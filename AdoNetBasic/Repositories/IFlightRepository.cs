using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IFlightRepository
{
    Task<Flight?> GetByFlightNoAsync(string flightNo);
    Task<Flight?> GetWithLongestDistanceAsync();
    Task<IReadOnlyList<Flight>> GetDepartingFromCountryAsync(string countryCode);
}
