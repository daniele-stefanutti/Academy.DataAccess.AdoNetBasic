using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class CountryRepository : ICountryRepository
{
    public async Task<Country?> GetByCountryCodeAsync(string CountryCode)
        => throw new NotImplementedException();
}
