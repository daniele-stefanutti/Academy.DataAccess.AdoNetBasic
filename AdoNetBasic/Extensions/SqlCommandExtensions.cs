using System.Data;
using System.Data.SqlClient;

namespace AdoNetBasic.Extensions;

internal static class SqlCommandExtensions
{
    public static IReadOnlyList<T> ExecuteReadCommand<T>(this SqlCommand command, SqlParameter parameter, Func<SqlDataReader, T> getResults)
        => command.ExecuteReadCommand(new SqlParameter[] { parameter }, getResults);

    public static IReadOnlyList<T> ExecuteReadCommand<T>(this SqlCommand command, SqlParameter[] parameters, Func<SqlDataReader, T> getResults)
    {
        using SqlConnection connection = new(Constants.ConnectionString);
        connection.Open();

        command.Connection = connection;
        command.Parameters.AddRange(parameters);

        using SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

        List<T> results = new();

        while (dataReader.Read())
            results.Add(getResults(dataReader));

        return results;
    }

    public static Task<IReadOnlyList<T>> ExecuteReadCommandAsync<T>(this SqlCommand command, Func<SqlDataReader, T> getResults)
        => command.ExecuteReadCommandAsync(Array.Empty<SqlParameter>(), getResults);

    public static Task<IReadOnlyList<T>> ExecuteReadCommandAsync<T>(this SqlCommand command, SqlParameter parameter, Func<SqlDataReader, T> getResults)
        => command.ExecuteReadCommandAsync(new SqlParameter[] { parameter }, getResults);

    public static async Task<IReadOnlyList<T>> ExecuteReadCommandAsync<T>(this SqlCommand command, SqlParameter[] parameters, Func<SqlDataReader, T> getResults)
    {
        using SqlConnection connection = new(Constants.ConnectionString);
        await connection.OpenAsync();

        command.Connection = connection;
        command.Parameters.AddRange(parameters);

        using SqlDataReader dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

        List<T> results = new();

        while (await dataReader.ReadAsync())
            results.Add(getResults(dataReader));

        return results;
    }

    public static int ExecuteWriteCommand(this SqlCommand command, SqlParameter parameter)
        => command.ExecuteWriteCommand(new SqlParameter[] { parameter });

    public static int ExecuteWriteCommand(this SqlCommand command, SqlParameter[] parameters)
    {
        using SqlConnection connection = new(Constants.ConnectionString);
        connection.Open();

        return command.ExecuteWriteCommand(connection, parameters);
    }

    public static int ExecuteWriteCommand(this SqlCommand command, IReadOnlyList<SqlParameter[]> parametersSets)
    {
        using SqlConnection connection = new(Constants.ConnectionString);
        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            command.Transaction = transaction;

            int rowsAffected = 0;

            foreach (SqlParameter[] parameters in parametersSets)
                rowsAffected += command.ExecuteWriteCommand(connection, parameters);

            transaction.Commit();
            return rowsAffected;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    private static int ExecuteWriteCommand(this SqlCommand command, SqlConnection connection, SqlParameter[] parameters)
    {
        command.Connection ??= connection;

        foreach (SqlParameter parameter in parameters)
            command.Parameters.CreateOrUpdateParameter(parameter);

        return command.ExecuteNonQuery();
    }
}
