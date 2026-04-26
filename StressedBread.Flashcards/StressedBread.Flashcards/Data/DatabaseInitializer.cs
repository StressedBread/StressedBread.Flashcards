using Spectre.Console;
using Microsoft.Data.SqlClient;

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

    internal bool IsDefaultConnectionStringValid()
    {
        if (!string.IsNullOrEmpty(_defaultConnectionString))
        {
            return true;
        }
        return false;
    }

    internal bool InitializeDatabase()
    {
        using var connection = new SqlConnection(_defaultConnectionString);
        connection.Open();

        var createDbQuery = @"
            IF DB_ID ('FlashcardsStressedBread') IS NULL
            BEGIN
                CREATE DATABASE FlashcardsStressedBread;
                SELECT 1;
            END   
            ELSE
                SELECT 0";

        using var cmd = new SqlCommand(createDbQuery, connection);
        var result = (int)cmd.ExecuteScalar();
        return result != 1;
    }

    internal (bool Stacks, bool Flashcards) CreateTables()
    {
        using var connection = new SqlConnection(_flashcardsConnectionString);
        connection.Open();

        var createStacksTableQuery = @"
            IF OBJECT_ID(N'dbo.Stacks', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Stacks (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(255) NOT NULL UNIQUE
                )
                SELECT 1;
            END
            ELSE
                SELECT 0";

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
                )
                SELECT 1;
            END
            ELSE
                SELECT 0";

        using var cmd = new SqlCommand(createStacksTableQuery, connection);
        using var cmd2 = new SqlCommand(createFlashcardsTableQuery, connection);
        var result1 = (int)cmd.ExecuteScalar();
        var result2 = (int)cmd2.ExecuteScalar();

        return (Stacks: result1 != 1, Flashcards: result2 != 1);
    }
}
