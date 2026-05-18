using Microsoft.Data.SqlClient;
using StressedBread.Flashcards.Data.Queries;
using StressedBread.Flashcards.Models;

namespace StressedBread.Flashcards.Data;

internal class DatabaseInitializer
{
    private readonly string _defaultConnectionString;
    private readonly string _flashcardsConnectionString;
    private readonly DatabaseInitQueries _databaseInitQueries;
    private readonly DatabaseAccess _defaultDatabaseAccess;
    private readonly DatabaseAccess _flashcardsDatabaseAccess;

    internal DatabaseInitializer(string connectionString, string flashcardsConnectionString, DatabaseInitQueries databaseInitQueries, DatabaseAccess defaultDatabaseAccess, DatabaseAccess flashcardsDatabaseAccess)
    {
        _defaultConnectionString = connectionString;
        _flashcardsConnectionString = flashcardsConnectionString;
        _databaseInitQueries = databaseInitQueries;
        _defaultDatabaseAccess = defaultDatabaseAccess;
        _flashcardsDatabaseAccess = flashcardsDatabaseAccess;
    }

    internal DatabaseSetupResultModel AreConnectionStringsValid()
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
            _defaultDatabaseAccess.ExecuteQuery(_databaseInitQueries.CreateDatabaseQuery(), _defaultConnectionString);
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
            _flashcardsDatabaseAccess.ExecuteQuery(_databaseInitQueries.CreateStacksTableQuery(), _flashcardsConnectionString);
            _flashcardsDatabaseAccess.ExecuteQuery(_databaseInitQueries.CreateFlashcardsTableQuery(), _flashcardsConnectionString);
            _flashcardsDatabaseAccess.ExecuteQuery(_databaseInitQueries.CreateStudySessionTableQuery(), _flashcardsConnectionString);

            return new DatabaseSetupResultModel { IsSuccessful = true };
        }
        catch (Exception ex)
        {
            return new DatabaseSetupResultModel { IsSuccessful = false, ErrorMessage = ex.Message };
        }
    }
}
