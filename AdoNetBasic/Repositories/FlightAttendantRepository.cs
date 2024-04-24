using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class FlightAttendantRepository : IFlightAttendantRepository
{
    #region QUERIES

    private const string SelectCommandText = @$"
        SELECT fa.* FROM {nameof(FlightAttendant)} fa
    ";

    #endregion

    #region READ

    public async Task<IReadOnlyList<FlightAttendant>> GetAllByFlightInstanceIdAsync(int flightInstanceId)
    {
        using SqlCommand command = new(@$"
            (
                {SelectCommandText}
                    LEFT JOIN {nameof(FlightInstance)} fi ON fi.{nameof(FlightInstance.Fsm_AttendantId)} = fa.{nameof(FlightAttendant.AttendantId)}
                WHERE fi.{nameof(FlightInstance.InstanceId)} = @{nameof(FlightInstance.InstanceId)}
            )
            UNION
            (
                {SelectCommandText}
                    LEFT JOIN {nameof(InstanceAttendant)} ia ON ia.{nameof(InstanceAttendant.AttendantId)} = fa.{nameof(FlightAttendant.AttendantId)}
                WHERE ia.{nameof(InstanceAttendant.InstanceId)} = @{nameof(FlightInstance.InstanceId)}
            );
        ");
        SqlParameter parameter = new($"@{nameof(FlightInstance.InstanceId)}", flightInstanceId);

        return await command.ExecuteReadCommandAsync(parameter, ParseFlightAttendantFromQueryResult);
    }

    public async Task<bool> IsMentorAsync(int attendantId)
    {
        using SqlCommand command = new(@$"
            SELECT COUNT(*) AS ""Count""
            FROM {nameof(FlightAttendant)} fa
            WHERE fa.{nameof(FlightAttendant.MentorId)} = @{nameof(FlightAttendant.AttendantId)};
        ");
        SqlParameter parameter = new($"@{nameof(FlightAttendant.AttendantId)}", attendantId);

        return (await command.ExecuteReadCommandAsync(parameter, dataReader => dataReader.GetInt32("Count") > 0)).Single();
    }

    #endregion

    #region Locals

    private static FlightAttendant ParseFlightAttendantFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            AttendantId = dataReader.GetInt32(nameof(FlightAttendant.AttendantId)),
            FirstName = dataReader.GetString(nameof(FlightAttendant.FirstName)),
            LastName = dataReader.GetString(nameof(FlightAttendant.LastName)),
            Dob = dataReader.GetDateTime(nameof(FlightAttendant.Dob)),
            HireDate = dataReader.GetDateTime(nameof(FlightAttendant.HireDate)),
            MentorId = dataReader.GetInt32(nameof(FlightAttendant.MentorId))
        };

    #endregion
}
