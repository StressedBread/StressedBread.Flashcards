namespace StressedBread.Flashcards.Data.Queries;
internal class FlashcardsQueries
{
    internal string GetFlashcardsQuery()
    {
        return @"
                SELECT Id, Question, Answer FROM Flashcards
                WHERE StackId = @StackId";
    }

    internal string AddFlashcardQuery()
    {
        return @"
                INSERT INTO Flashcards (Question, Answer, StackId) 
                VALUES (@Question, @Answer, @StackId)";
    }

    internal string DeleteFlashcardQuery()
    {
        return @"
                DELETE FROM Flashcards 
                WHERE Id = @Id";
    }

    internal string EditFlashcardQuery()
    {
        return @"
                UPDATE Flashcards 
                SET Question = @Question, Answer = @Answer 
                WHERE Id = @Id";
    }
}