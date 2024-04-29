using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public class CountryRepository : ICountryRepository
{
    public async Task<Country?> GetByCountryCodeAsync(string CountryCode)
        => throw new NotImplementedException();
}
