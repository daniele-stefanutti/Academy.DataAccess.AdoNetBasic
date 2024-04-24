using AdoNetBasic.Extensions;
using AdoNetBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Repositories;

public class PlaneDetailRepository : IPlaneDetailRepository
{
    #region QUERIES

    private const string SelectCommandText = @$"
        SELECT pd.* FROM {nameof(PlaneDetail)} pd
    ";

    #endregion

    #region READ

    public async Task<PlaneDetail?> GetByPlaneIdAsync(int planeId)
    {
        using SqlCommand command = new(@$"
            {SelectCommandText}
            WHERE pd.{nameof(PlaneDetail.PlaneId)} = @{nameof(PlaneDetail.PlaneId)};
        ");
        SqlParameter parameter = new($"@{nameof(PlaneDetail.PlaneId)}", planeId);

        return (await command.ExecuteReadCommandAsync(parameter, ParsePlaneDetailFromQueryResult)).FirstOrDefault();
    }

    #endregion

    #region Locals

    private static PlaneDetail ParsePlaneDetailFromQueryResult(SqlDataReader dataReader)
        => new()
        {
            PlaneId = dataReader.GetInt32(nameof(PlaneDetail.PlaneId)),
            ModelNumber = dataReader.GetString(nameof(PlaneDetail.ModelNumber)),
            RegistrationNo = dataReader.GetString(nameof(PlaneDetail.RegistrationNo)),
            BuiltYear = dataReader.GetInt16(nameof(PlaneDetail.BuiltYear)),
            FirstClassCapacity = dataReader.GetInt16(nameof(PlaneDetail.FirstClassCapacity)),
            EcoCapacity = dataReader.GetInt16(nameof(PlaneDetail.EcoCapacity))
        };

    #endregion
}
