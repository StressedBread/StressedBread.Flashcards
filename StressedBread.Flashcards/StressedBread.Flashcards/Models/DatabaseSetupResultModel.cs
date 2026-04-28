namespace StressedBread.Flashcards.Models;

internal class DatabaseSetupResultModel
{
    public bool IsSuccessful { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
