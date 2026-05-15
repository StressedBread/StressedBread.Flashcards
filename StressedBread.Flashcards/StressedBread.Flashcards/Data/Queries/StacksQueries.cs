namespace StressedBread.Flashcards.Data.Queries;
internal class StacksQueries
{
    internal string GetAllStacksQuery()
    {
        return "SELECT * FROM dbo.Stacks ORDER BY Name;";
    }

    internal string CreateStackQuery()
    {
        return "INSERT INTO dbo.Stacks (Name) VALUES (@Name);";
    }

    internal string DeleteStackQuery()
    {
        return "DELETE FROM dbo.Stacks WHERE Id = @Id;";
    }
}
