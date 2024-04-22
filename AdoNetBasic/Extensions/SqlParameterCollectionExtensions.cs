using System.Data.SqlClient;

namespace AdoNetBasic.Extensions;

internal static class SqlParameterCollectionExtensions
{
    public static void CreateOrUpdateParameter(this SqlParameterCollection parameters, string propertyName, object parameterValue)
    {
        string parameterName = $"@{propertyName}";

        if (parameters.Contains(parameterName))
            parameters[parameterName].Value = parameterValue;
        else
            parameters.Add(new(parameterName, parameterValue));
    }

    public static void CreateOrUpdateParameter(this SqlParameterCollection parameters, SqlParameter parameter)
    {
        if (parameters.Contains(parameter.ParameterName))
            parameters[parameter.ParameterName].Value = parameter.Value;
        else
            parameters.Add(parameter);
    }
}
