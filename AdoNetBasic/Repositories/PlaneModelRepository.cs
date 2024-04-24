using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class PlaneModelRepository : IPlaneModelRepository
{
    #region QUERIES

    private const string SelectCommandText = @$"
        SELECT pm.* FROM {nameof(PlaneModel)} pm
    ";

    #endregion

    #region READ

    public async Task<PlaneModel?> GetByModelNumberAsync(string modelNumber)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
            WHERE pm.{nameof(PlaneModel.ModelNumber)} = @{nameof(PlaneModel.ModelNumber)};
        ");
        SqlParameter parameter = new($"@{nameof(PlaneModel.ModelNumber)}", modelNumber);

        return (await command.ExecuteReadCommandAsync(parameter, ParsePlaneModelFromQueryResult)).FirstOrDefault();
    }

    #endregion

    #region Locals

    private static PlaneModel ParsePlaneModelFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            ModelNumber = dataReader.GetString(nameof(PlaneModel.ModelNumber)),
            ManufacturerName = dataReader.GetString(nameof(PlaneModel.ManufacturerName)),
            PlaneRange = dataReader.GetInt16(nameof(PlaneModel.PlaneRange)),
            CruiseSpeed = dataReader.GetInt16(nameof(PlaneModel.CruiseSpeed))
        };

    #endregion
}
