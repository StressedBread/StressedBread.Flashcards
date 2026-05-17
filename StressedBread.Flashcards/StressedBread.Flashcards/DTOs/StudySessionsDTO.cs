namespace StressedBread.Flashcards.DTOs;
internal class StudySessionsDTO
{
    public int Id { get; set; }
    public int Score { get; set; }
    public DateTime SessionDate { get; set; }
    public string StackName { get; set; } = string.Empty;
}
