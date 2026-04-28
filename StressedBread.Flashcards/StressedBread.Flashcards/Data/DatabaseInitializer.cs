using Microsoft.Data.SqlClient;
using StressedBread.Flashcards.Models;

namespace StressedBread.Flashcards.Data;

internal class DatabaseInitializer
{
    private readonly string _defaultConnectionString;
    private readonly string _flashcardsConnectionString;

    internal DatabaseInitializer(string connectionString, string flashcardsConnectionString)
    {
        _defaultConnectionString = connectionString;
        _flashcardsConnectionString = flashcardsConnectionString;
    }

    internal DatabaseSetupResultModel IsDefaultConnectionStringValid()
    {
        if (!string.IsNullOrWhiteSpace(_defaultConnectionString) && !string.IsNullOrWhiteSpace(_flashcardsConnectionString))
        {
            return new DatabaseSetupResultModel { IsSuccessful = true };
        }

        return new DatabaseSetupResultModel { IsSuccessful = false };
    }

    internal DatabaseSetupResultModel InitializeDatabase()
    {
        try
        {
            using var connection = new SqlConnection(_defaultConnectionString);
            connection.Open();

            var createDbQuery = @"
            IF DB_ID ('FlashcardsStressedBread') IS NULL
            BEGIN
                CREATE DATABASE FlashcardsStressedBread;
            END";

            using var cmd = new SqlCommand(createDbQuery, connection);
            cmd.ExecuteNonQuery();
            return new DatabaseSetupResultModel { IsSuccessful = true };
        }
        catch (Exception ex)
        {
            return new DatabaseSetupResultModel { IsSuccessful = false, ErrorMessage = ex.Message };
        }
    }

    internal DatabaseSetupResultModel CreateTables()
    {
        try
        {
            using var connection = new SqlConnection(_flashcardsConnectionString);
            connection.Open();

            var createStacksTableQuery = @"
            IF OBJECT_ID(N'dbo.Stacks', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Stacks (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(255) NOT NULL UNIQUE
                );
            END";

            var createFlashcardsTableQuery = @"
            IF OBJECT_ID(N'dbo.Flashcards', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Flashcards (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Question NVARCHAR(255) NOT NULL,
                    Answer NVARCHAR(255) NOT NULL,
                    StackId INT NOT NULL,
                    CONSTRAINT FK_Flashcards_Stacks
                        FOREIGN KEY (StackId) 
                        REFERENCES dbo.Stacks(Id)
                        ON DELETE CASCADE
                );
            END";

            using var cmd = new SqlCommand(createStacksTableQuery, connection);
            using var cmd2 = new SqlCommand(createFlashcardsTableQuery, connection);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            return new DatabaseSetupResultModel { IsSuccessful = true };
        }
        catch (Exception ex)
        {
            return new DatabaseSetupResultModel { IsSuccessful = false, ErrorMessage = ex.Message };
        }
    }
}
