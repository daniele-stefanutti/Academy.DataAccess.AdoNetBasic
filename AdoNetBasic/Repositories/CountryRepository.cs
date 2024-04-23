using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class CountryRepository : ICountryRepository
{
    #region QUERIES

    private const string SelectCommandText = @$"
        SELECT * FROM {nameof(Country)}
    ";

    private const string SelectByCountryCodeCommandText = @$"
        {SelectCommandText} WHERE {nameof(Country.CountryCode)} = @{nameof(Country.CountryCode)}
    ";

    #endregion

    #region READ

    public async Task<Country?> GetByCountryCodeAsync(string CountryCode)
    {
        using SqlCommand command = new(SelectByCountryCodeCommandText);
        SqlParameter parameter = new($"@{nameof(Country.CountryCode)}", CountryCode);

        return (await command.ExecuteReadCommandAsync(parameter, ParseCountryFromQueryResult)).FirstOrDefault();
    }

    #endregion

    #region Locals

    private static Country ParseCountryFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            CountryCode = dataReader.GetString(nameof(Country.CountryCode)),
            CountryName = dataReader.GetString(nameof(Country.CountryName))
        };

    #endregion
}
