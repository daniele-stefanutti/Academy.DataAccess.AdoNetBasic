using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IFlightRepository
{
    Task<Flight?> GetWithLongestDistanceAsync();
    Task<IReadOnlyList<Flight>> GetDepartingFromCountryAsync(string countryCode);
}
