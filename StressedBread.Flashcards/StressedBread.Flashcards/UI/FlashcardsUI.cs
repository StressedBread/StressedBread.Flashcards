using Spectre.Console;
using StressedBread.Flashcards.DTOs;

namespace StressedBread.Flashcards.UI;
internal class FlashcardsUI
{
    internal int FlashcardsStackView(List<FlashcardsDTO> flashcards)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Gray);

        table.AddColumn("ID", col => col.LeftAligned());
        table.AddColumn("Question", col => col.LeftAligned());
        table.AddColumn("Answer", col => col.LeftAligned());

        int displayId = 1;

        foreach (var flashcard in flashcards)
        {
            table.AddRow(displayId.ToString(), flashcard.Question, flashcard.Answer);
            displayId++;
        }

        AnsiConsole.Write(table);

        return AnsiConsole.Ask<int>("Enter the [blue]ID[/] of a flashcard or enter [blue]0[/] to go back:");
    }

    internal (string question, string answer) AddFlashcardView()
    {
        AnsiConsole.Clear();
        string question = AnsiConsole.Ask<string>("Enter the [blue]question[/] for the new flashcard:");
        string answer = AnsiConsole.Ask<string>("Enter the [blue]answer[/] for the new flashcard:");
        return (question, answer);
    }

    internal (string question, string answer) EditFlashcardView(FlashcardsDTO flashcard)
    {
        AnsiConsole.Clear();
        string question = AnsiConsole.Ask<string>($"Enter the new [blue]question[/] for the flashcard (current: [blue]{flashcard.Question}[/]) If you want to keep current, enter 0:");
        string answer = AnsiConsole.Ask<string>($"Enter the new [blue]answer[/] for the flashcard (current: [blue]{flashcard.Answer}[/]) If you want to keep current, enter 0:");
        return (question, answer);
    }

}
