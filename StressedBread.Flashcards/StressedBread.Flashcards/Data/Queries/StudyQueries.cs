namespace StressedBread.Flashcards.Data.Queries;
internal class StudyQueries
{
    internal string InsertStudySessionQuery()
    {
        return @"
            INSERT INTO dbo.StudySessions (Score, SessionDate, StackId)
            VALUES (@Score, @SessionDate, @StackId);";
    }
    internal string GetStudySessionsByStackIdQuery()
    {
        return @"
            SELECT * FROM dbo.StudySessions
            WHERE StackId = @StackId;";
    }
}
