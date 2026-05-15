namespace StressedBread.Flashcards.DTOs;
internal class AllFlashcardsDTO
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string StackName { get; set; } = string.Empty;
}
