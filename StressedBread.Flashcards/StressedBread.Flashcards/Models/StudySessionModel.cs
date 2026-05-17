namespace StressedBread.Flashcards.Models;
internal class StudySessionModel
{
    public int Id { get; set; }
    public int Score { get; set; }
    public DateTime SessionDate { get; set; }
    public int StackId { get; set; }
}
