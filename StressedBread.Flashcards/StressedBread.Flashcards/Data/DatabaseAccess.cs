using Dapper;
using Microsoft.Data.SqlClient;

namespace StressedBread.Flashcards.Data;
internal class DatabaseAccess
{
    private readonly string _connectionString;

    public DatabaseAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

    internal void ExecuteQuery (string query, object? parameters = null)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        connection.Execute(query, parameters);
    }

    internal List<T> Reader<T>(string query, object? parameters = null)
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection.Query<T>(query, parameters).ToList();
    }
}
