namespace StressedBread.Flashcards.Models;
internal class ReportModel
{
    public string StackName { get; set; } = string.Empty;
    public Dictionary<string, int> MonthlyValues { get; set; } = new();
}
