using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

/// <remarks>
/// Please, complete the implementation of this class
/// </remarks>
public class FlightInstanceRepository : IFlightInstanceRepository
{
    #region QUERIES

    private const string SelectCommandText = @$"
        SELECT * FROM {nameof(FlightInstance)}
    ";

    private const string SelectByInstanceIdCommandText = @$"
        {SelectCommandText} WHERE {nameof(FlightInstance.InstanceId)} = @{nameof(FlightInstance.InstanceId)}
    ";

    #endregion

    #region READ

    public async Task<FlightInstance?> GetByIdAsync(int instanceId)
    {
        using SqlCommand command = new(SelectByInstanceIdCommandText);
        SqlParameter parameter = new($"@{nameof(FlightInstance.InstanceId)}", instanceId);

        return (await command.ExecuteReadCommandAsync(parameter, ParseFlightInstanceFromQueryResult)).FirstOrDefault();
    }

    #endregion

    #region Locals

    private static FlightInstance ParseFlightInstanceFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            InstanceId = dataReader.GetInt32(nameof(FlightInstance.InstanceId)),
            CoPilotAboardId = dataReader.GetInt32(nameof(FlightInstance.CoPilotAboardId)),
            DateTimeArrive = dataReader.GetDateTime(nameof(FlightInstance.DateTimeArrive))
        };

    #endregion
}
