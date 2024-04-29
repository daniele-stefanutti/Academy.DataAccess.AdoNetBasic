using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public class FlightRepository : IFlightRepository
{
    public async Task<Flight?> GetWithLongestDistanceAsync()
        => throw new NotImplementedException();

    public async Task<IReadOnlyList<Flight>> GetDepartingFromCountryAsync(string countryCode)
        => throw new NotImplementedException();
}
