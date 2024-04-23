﻿using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class FlightRepository : IFlightRepository
{
    #region READ

    public async Task<Flight?> GetWithLongestDistanceAsync()
    {
        using SqlCommand command = new(@$"
            SELECT TOP 1 *
            FROM {nameof(Flight)}
            ORDER BY [{nameof(Flight.Distance)}] DESC
        ");

        return (await command.ExecuteReadCommandAsync(ParseFlightFromQueryResult)).FirstOrDefault();
    }

    public async Task<IReadOnlyList<Flight>> GetDepartingFromCountryAsync(string countryCode)
    {
        using SqlCommand command = new(@$"
            SELECT f.*
            FROM {nameof(Flight)} f
                LEFT JOIN {nameof(Airport)} a ON a.{nameof(Airport.AirportCode)} = f.{nameof(Flight.FlightArriveFrom)}
            WHERE a.{nameof(Airport.CountryCode)} = @{nameof(Airport.CountryCode)}
        ");
        SqlParameter parameter = new($"@{nameof(Airport.CountryCode)}", countryCode);

        return await command.ExecuteReadCommandAsync(parameter, ParseFlightFromQueryResult);
    }

    #endregion

    #region Locals

    private static Flight ParseFlightFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            FlightNo = dataReader.GetString(nameof(Flight.FlightNo)),
            FlightDepartTo = dataReader.GetString(nameof(Flight.FlightDepartTo)),
            FlightArriveFrom = dataReader.GetString(nameof(Flight.FlightArriveFrom)),
            Distance = dataReader.GetInt32(nameof(Flight.Distance))
        };

    #endregion
}