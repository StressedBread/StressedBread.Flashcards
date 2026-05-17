namespace StressedBread.Flashcards.Data.Queries;
internal class DatabaseInitQueries
{
    internal string CreateDatabaseQuery()
    {
        return @"
            IF DB_ID ('FlashcardsStressedBread') IS NULL
            BEGIN
                CREATE DATABASE FlashcardsStressedBread;
            END";
    }

    internal string CreateStacksTableQuery()
    {
        return @"
            IF OBJECT_ID(N'dbo.Stacks', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Stacks (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(255) NOT NULL UNIQUE
                );
            END";
    }

    internal string CreateFlashcardsTableQuery()
    {
        return @"
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
    }

    internal string CreateStudySessionTableQuery()
    {
        return @"
            IF OBJECT_ID(N'dbo.StudySessions', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.StudySessions (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Score INT NOT NULL,
                    SessionDate DATETIME NOT NULL,
                    StackId INT NOT NULL,
                    CONSTRAINT FK_StudySessions_Stacks
                        FOREIGN KEY (StackId) 
                        REFERENCES dbo.Stacks(Id)
                        ON DELETE CASCADE
                );
            END";
    }
}
