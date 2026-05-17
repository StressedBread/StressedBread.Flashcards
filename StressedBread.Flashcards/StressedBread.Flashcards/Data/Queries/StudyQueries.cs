namespace StressedBread.Flashcards.Data.Queries;
internal class StudyQueries
{
    internal string InsertStudySessionQuery()
    {
        return @"
            INSERT INTO dbo.StudySessions (Score, SessionDate, StackId)
            VALUES (@Score, @SessionDate, @StackId);";
    }
    internal string GetStudySessionsQuery()
    {
        return @"
            SELECT 
                study.Id, 
                study.Score, 
                study.SessionDate, 
                stack.Name AS StackName,
                study.StackId 
            FROM dbo.StudySessions study
            JOIN dbo.Stacks stack ON study.StackId = stack.Id   
            WHERE (@StackId IS NULL OR study.StackId = @StackId);";
    }
}
