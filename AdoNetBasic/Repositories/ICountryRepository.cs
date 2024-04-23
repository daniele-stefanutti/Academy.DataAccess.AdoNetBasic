using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface ICountryRepository
{
    Task<Country?> GetByCountryCodeAsync(string countryCode);
}
