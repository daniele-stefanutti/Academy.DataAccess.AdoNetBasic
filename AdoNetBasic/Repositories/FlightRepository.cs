using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class FlightRepository : IFlightRepository
{
    public async Task<Flight?> GetWithLongestDistanceAsync()
        => throw new NotImplementedException();

    public async Task<IReadOnlyList<Flight>> GetDepartingFromCountryAsync(string countryCode)
        => throw new NotImplementedException();
}
