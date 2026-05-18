using Dapper;
using Microsoft.Data.SqlClient;
using StressedBread.Flashcards.Models;

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
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection.Query<T>(query, parameters).ToList();
    }

    internal ReportModel? ReportQuery(string query, object? parameters = null)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        dynamic? row = connection.QuerySingleOrDefault<dynamic>(query, parameters);
        if (row == null) return null;

        IDictionary<string, object> dict = (IDictionary<string, object>)row;
        ReportModel report = new ReportModel();

        List<string> keys = dict.Keys.ToList();

        report.StackName = dict[keys[0]].ToString() ?? String.Empty;

        foreach (var key in keys.Skip(1))
        {
            if (int.TryParse(dict[key].ToString(), out int value))
                report.MonthlyValues[key] = value;
        }

        return report;
    }
}
