using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class AirportRepository : IAirportRepository
{
    #region QUERIES

    private const string SelectCommandText = @$"
        SELECT * FROM {nameof(Airport)}
    ";

    private const string SelectByAirportCodeCommandText = @$"
        {SelectCommandText} WHERE {nameof(Airport.AirportCode)} = @{nameof(Airport.AirportCode)}
    ";

    private const string InsertCommandText = @$"
        INSERT INTO {nameof(Airport)}
        (
            [{nameof(Airport.AirportCode)}],
            [{nameof(Airport.AirportName)}],
            [{nameof(Airport.ContactNo)}],
            [{nameof(Airport.Longitude)}],
            [{nameof(Airport.Latitude)}],
            [{nameof(Airport.CountryCode)}]
        )
        VALUES
        (
            @{nameof(Airport.AirportCode)},
            @{nameof(Airport.AirportName)},
            @{nameof(Airport.ContactNo)},
            @{nameof(Airport.Longitude)},
            @{nameof(Airport.Latitude)},
            @{nameof(Airport.CountryCode)}
        );";

    private const string UpdateCommandText = $@"
        UPDATE {nameof(Airport)}
        SET
            [{nameof(Airport.AirportName)}] = @{nameof(Airport.AirportName)},
            [{nameof(Airport.ContactNo)}] = @{nameof(Airport.ContactNo)},
            [{nameof(Airport.Longitude)}] = @{nameof(Airport.Longitude)},
            [{nameof(Airport.Latitude)}] = @{nameof(Airport.Latitude)},
            [{nameof(Airport.CountryCode)}] = @{nameof(Airport.CountryCode)}
        WHERE [{nameof(Airport.AirportCode)}] = @{nameof(Airport.AirportCode)};";

    private const string DeleteCommandText = $@"
        DELETE FROM {nameof(Airport)}";

    #endregion

    #region READ

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    public Airport? GetByAirportCode(string airportCode)
    {
        using SqlCommand command = new(SelectByAirportCodeCommandText);
        SqlParameter parameter = new($"@{nameof(Airport.AirportCode)}", airportCode);

        return command.ExecuteReadCommand(parameter, ParseAirportFromQueryResult).FirstOrDefault();
    }

    public async Task<Airport?> GetByAirportCodeAsync(string airportCode)
    {
        using SqlCommand command = new(SelectByAirportCodeCommandText);
        SqlParameter parameter = new($"@{nameof(Airport.AirportCode)}", airportCode);

        return (await command.ExecuteReadCommandAsync(parameter, ParseAirportFromQueryResult)).FirstOrDefault();
    }

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    public IReadOnlyList<Airport> GetByCountryCode(string countryCode)
    {
        using SqlCommand command = new($"{SelectCommandText} WHERE {nameof(Airport.CountryCode)} = @{nameof(Airport.CountryCode)}");
        SqlParameter parameter = new($"@{nameof(Airport.CountryCode)}", countryCode);

        return command.ExecuteReadCommand(parameter, ParseAirportFromQueryResult);
    }

    /// <remarks>
    /// Please, implement this method!
    /// 
    /// North West
    ///     \_________
    ///     |         |
    ///     |         |
    ///     |         |
    ///     |_________|
    ///                \
    ///            South East
    /// 
    /// </remarks>
    public IReadOnlyList<Airport> GetBySquareArea(double northWestLongitude, double northWestLatitude, double southEastLongitude, double southEastLatitude)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
            WHERE {nameof(Airport.Longitude)} >= @NorthWestLongitude and {nameof(Airport.Longitude)} <= @SouthEastLongitude and 
                  {nameof(Airport.Latitude)} >= @NorthWestLatitude and {nameof(Airport.Latitude)} <= @SouthEastLatitude");
        SqlParameter[] parameters = new SqlParameter[]
        {
            new("@NorthWestLongitude", northWestLongitude),
            new("@NorthWestLatitude", northWestLatitude),
            new("@SouthEastLongitude", southEastLongitude),
            new("@SouthEastLatitude", southEastLatitude)
        };

        return command.ExecuteReadCommand(parameters, ParseAirportFromQueryResult);
    }

    #endregion

    #region CREATE

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int Add(Airport airport)
    {
        using SqlCommand command = new(InsertCommandText);
        SqlParameter[] parameters = GetAirportParameters(airport);

        return command.ExecuteWriteCommand(parameters);
    }

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int AddRange(IReadOnlyList<Airport> airports)
    {
        using SqlCommand command = new(InsertCommandText);
        IReadOnlyList<SqlParameter[]> parametersSet = airports.Select(a => GetAirportParameters(a)).ToList();

        return command.ExecuteWriteCommand(parametersSet);
    }

    #endregion

    #region UPDATE

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int Update(Airport airport)
    {
        using SqlCommand command = new(UpdateCommandText);
        SqlParameter[] parameters = GetAirportParameters(airport);

        return command.ExecuteWriteCommand(parameters);
    }

    #endregion

    #region DELETE

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int DeleteByAirportCode(string airportCode)
    {
        using SqlCommand command = new($"{DeleteCommandText} WHERE {nameof(Airport.AirportCode)} = @{nameof(Airport.AirportCode)}");
        SqlParameter parameter = new(nameof(Airport.AirportCode), airportCode);

        return command.ExecuteWriteCommand(parameter);
    }

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int DeleteByCountryCode(string countryCode)
    {
        using SqlCommand command = new($"{DeleteCommandText} WHERE {nameof(Airport.CountryCode)} = @{nameof(Airport.CountryCode)}");
        SqlParameter parameter = new(nameof(Airport.CountryCode), countryCode);

        return command.ExecuteWriteCommand(parameter);
    }

    #endregion

    #region Locals

    private static Airport ParseAirportFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            AirportCode = dataReader.GetString(nameof(Airport.AirportCode)),
            AirportName = dataReader.GetString(nameof(Airport.AirportName)),
            ContactNo = dataReader.GetDecimal(nameof(Airport.ContactNo)),
            Longitude = dataReader.GetDouble(nameof(Airport.Longitude)),
            Latitude = dataReader.GetDouble(nameof(Airport.Latitude)),
            CountryCode = dataReader.GetString(nameof(Airport.CountryCode))
        };

    private static SqlParameter[] GetAirportParameters(Airport airport)
        => new SqlParameter[]
        {
            new(nameof(Airport.AirportCode), airport.AirportCode),
            new(nameof(Airport.AirportName), airport.AirportName),
            new(nameof(Airport.ContactNo), airport.ContactNo),
            new(nameof(Airport.Longitude), airport.Longitude),
            new(nameof(Airport.Latitude), airport.Latitude),
            new(nameof(Airport.CountryCode), airport.CountryCode)
        };

    #endregion
}
