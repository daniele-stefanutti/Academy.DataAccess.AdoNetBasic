using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class PilotRepository : IPilotRepository
{
    #region QUERIES

    private const string SelectCommandText = @$"
        SELECT p.* FROM {nameof(Pilot)} p
    ";

    #endregion

    #region READ

    public async Task<Pilot?> GetByPilotIdAsync(int pilotId)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
            WHERE p.{nameof(Pilot.PilotId)} = @{nameof(Pilot.PilotId)};
        ");
        SqlParameter parameter = new($"@{nameof(Pilot.PilotId)}", pilotId);

        return (await command.ExecuteReadCommandAsync(parameter, ParsePilotFromQueryResult)).FirstOrDefault();
    }

    public async Task<Pilot?> GetByFirstNameAndLastNameAsync(string firstName, string lastName)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
            WHERE p.{nameof(Pilot.FirstName)} = @{nameof(Pilot.FirstName)} AND p.{nameof(Pilot.LastName)} = @{nameof(Pilot.LastName)};
        ");
        SqlParameter[] parameters = new SqlParameter[]
        {
            new($"@{nameof(Pilot.FirstName)}", firstName),
            new($"@{nameof(Pilot.LastName)}", lastName)
        };

        return (await command.ExecuteReadCommandAsync(parameters, ParsePilotFromQueryResult)).FirstOrDefault();
    }

    #endregion

    #region Locals

    private static Pilot ParsePilotFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            PilotId = dataReader.GetInt32(nameof(Pilot.PilotId)),
            FirstName = dataReader.GetString(nameof(Pilot.FirstName)),
            LastName = dataReader.GetString(nameof(Pilot.LastName)),
            Dob = dataReader.GetDateTime(nameof(Pilot.Dob)),
            HoursFlown = dataReader.GetInt16(nameof(Pilot.HoursFlown))
        };

    #endregion
}
