namespace StressedBread.Flashcards;

internal class Enums
{
    internal enum MenuOption
    {
        ManageStacks,
        ManageFlashcards,
        Study,
        ViewStudySessions,
        ViewReports,
        Exit
    }

    internal enum StackMenuOption
    {
        ViewFlashcards,
        AddFlashcard,
        EditFlashcard,
        DeleteFlashcard,
        ChangeStack,
        DeleteStack,
        BackToMainMenu
    }

    internal enum FlashcardMenuOption
    {
        EditFlashcard,
        DeleteFlashcard,
        BackToMainMenu
    }

    internal enum StackReturnOption
    {
        ChangeStack,
        BackToMainMenu
    }

    internal enum ReportMenuOption
    {
        SessionsPerMonthPerStack,
        AverageScorePerMonthPerStack,
        BackToMainMenu
    }
}