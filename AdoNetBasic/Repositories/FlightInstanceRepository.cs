using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class FlightInstanceRepository : IFlightInstanceRepository
{
    #region QUERIES

    private const string SelectCommandText = @$"
        SELECT fi.* FROM {nameof(FlightInstance)} fi
    ";

    private const string SelectByInstanceIdCommandText = @$"
        {SelectCommandText} WHERE fi.{nameof(FlightInstance.InstanceId)} = @{nameof(FlightInstance.InstanceId)}
    ";

    private const string UpdateCommandText = $@"
        UPDATE {nameof(FlightInstance)}
        SET
            {nameof(FlightInstance.FlightNo)} = @{nameof(FlightInstance.FlightNo)},
            {nameof(FlightInstance.PlaneId)} = @{nameof(FlightInstance.PlaneId)},
            {nameof(FlightInstance.PilotAboardId)} = @{nameof(FlightInstance.PilotAboardId)},
            {nameof(FlightInstance.CoPilotAboardId)} = @{nameof(FlightInstance.CoPilotAboardId)},
            {nameof(FlightInstance.Fsm_AttendantId)} = @{nameof(FlightInstance.Fsm_AttendantId)},
            {nameof(FlightInstance.DateTimeLeave)} = @{nameof(FlightInstance.DateTimeLeave)},
            {nameof(FlightInstance.DateTimeArrive)} = @{nameof(FlightInstance.DateTimeArrive)}
        WHERE {nameof(FlightInstance.InstanceId)} = @{nameof(FlightInstance.InstanceId)};";

    #endregion

    #region READ

    public async Task<FlightInstance?> GetByInstanceIdAsync(int instanceId)
    {
        using SqlCommand command = new(SelectByInstanceIdCommandText);
        SqlParameter parameter = new($"@{nameof(FlightInstance.InstanceId)}", instanceId);

        return (await command.ExecuteReadCommandAsync(parameter, ParseFlightInstanceFromQueryResult)).FirstOrDefault();
    }

    public async Task<FlightInstance?> GetByFlightNoAndDateTimeLeaveAsync(string flightNo, DateTime dateTimeLeave)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
            WHERE fi.{nameof(FlightInstance.FlightNo)} = @{nameof(FlightInstance.FlightNo)}
                AND fi.{nameof(FlightInstance.DateTimeLeave)} = @{nameof(FlightInstance.DateTimeLeave)};
        ");
        SqlParameter[] parameters = new SqlParameter[]
        {
            new($"@{nameof(FlightInstance.FlightNo)}", flightNo),
            new($"@{nameof(FlightInstance.DateTimeLeave)}", dateTimeLeave)
        };

        return (await command.ExecuteReadCommandAsync(parameters, ParseFlightInstanceFromQueryResult)).FirstOrDefault();
    }

    public async Task<IReadOnlyList<FlightInstance>> GetWithinDateTimeLeaveRangeAsync(DateTime startDateTimeLeave, DateTime endDateTimeLeave)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
            WHERE fi.{nameof(FlightInstance.DateTimeLeave)} > @{nameof(startDateTimeLeave)}
                AND fi.{nameof(FlightInstance.DateTimeLeave)} < @{nameof(endDateTimeLeave)};
        ");
        SqlParameter[] parameters = new SqlParameter[]
        {
            new($"@{nameof(startDateTimeLeave)}", startDateTimeLeave),
            new($"@{nameof(endDateTimeLeave)}", endDateTimeLeave)
        };

        return await command.ExecuteReadCommandAsync(parameters, ParseFlightInstanceFromQueryResult);
    }

    public async Task<IReadOnlyList<FlightInstance>> GetByPlaneManufacturerNameAsync(string planeManufacturerName)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
                LEFT JOIN {nameof(PlaneDetail)} pd ON pd.{nameof(PlaneDetail.PlaneId)} = fi.{nameof(FlightInstance.PlaneId)}
                LEFT JOIN {nameof(PlaneModel)} pm ON pm.{nameof(PlaneModel.ModelNumber)} = pd.{nameof(PlaneDetail.ModelNumber)}
            WHERE pm.{nameof(PlaneModel.ManufacturerName)} = @{nameof(PlaneModel.ManufacturerName)};
        ");
        SqlParameter parameter = new($"@{nameof(PlaneModel.ManufacturerName)}", planeManufacturerName);

        return await command.ExecuteReadCommandAsync(parameter, ParseFlightInstanceFromQueryResult);
    }

    public async Task<IReadOnlyList<FlightInstance>> GetByFlightArriveFromAirportCodeAsync(string airportCode)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
                LEFT JOIN {nameof(Flight)} f ON f.{nameof(Flight.FlightNo)} = fi.{nameof(FlightInstance.FlightNo)}
            WHERE f.{nameof(Flight.FlightArriveFrom)} = @{nameof(Airport.AirportCode)};
        ");
        SqlParameter parameter = new($"@{nameof(Airport.AirportCode)}", airportCode);

        return await command.ExecuteReadCommandAsync(parameter, ParseFlightInstanceFromQueryResult);
    }

    #endregion

    #region UPDATE

    public async Task<int> UpdateAsync(FlightInstance flightInstance)
    {
        using SqlCommand command = new(UpdateCommandText);
        SqlParameter[] parameters = GetFlightInstanceParameters(flightInstance);

        return await command.ExecuteWriteCommandAsync(parameters);
    }

    public async Task<int> UpdateRangeAsync(IReadOnlyList<FlightInstance> flightInstances)
    {
        using SqlCommand command = new(UpdateCommandText);
        IReadOnlyList<SqlParameter[]> parametersSet = flightInstances.Select(GetFlightInstanceParameters).ToList();

        return await command.ExecuteWriteCommandAsync(parametersSet);
    }

    #endregion

    #region Locals

    private static FlightInstance ParseFlightInstanceFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            InstanceId = dataReader.GetInt32(nameof(FlightInstance.InstanceId)),
            FlightNo = dataReader.GetString(nameof(FlightInstance.FlightNo)),
            PlaneId = dataReader.GetInt32(nameof(FlightInstance.PlaneId)),
            PilotAboardId = dataReader.GetInt32(nameof(FlightInstance.PilotAboardId)),
            CoPilotAboardId = dataReader.GetInt32(nameof(FlightInstance.CoPilotAboardId)),
            Fsm_AttendantId = dataReader.GetInt32(nameof(FlightInstance.Fsm_AttendantId)),
            DateTimeLeave = dataReader.GetDateTime(nameof(FlightInstance.DateTimeLeave)),
            DateTimeArrive = dataReader.GetDateTime(nameof(FlightInstance.DateTimeArrive))
        };

    private static SqlParameter[] GetFlightInstanceParameters(FlightInstance flightInstance)
        => new SqlParameter[]
        {
            new(nameof(FlightInstance.InstanceId), flightInstance.InstanceId),
            new(nameof(FlightInstance.FlightNo), flightInstance.FlightNo),
            new(nameof(FlightInstance.PlaneId), flightInstance.PlaneId),
            new(nameof(FlightInstance.PilotAboardId), flightInstance.PilotAboardId),
            new(nameof(FlightInstance.CoPilotAboardId), flightInstance.CoPilotAboardId),
            new(nameof(FlightInstance.Fsm_AttendantId), flightInstance.Fsm_AttendantId),
            new(nameof(FlightInstance.DateTimeLeave), flightInstance.DateTimeLeave),
            new(nameof(FlightInstance.DateTimeArrive), flightInstance.DateTimeArrive)
        };

    #endregion
}
