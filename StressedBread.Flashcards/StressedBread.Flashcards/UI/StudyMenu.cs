using Spectre.Console;
using StressedBread.Flashcards.DTOs;
using StressedBread.Flashcards.Models;

namespace StressedBread.Flashcards.UI;
internal class StudyMenu
{
    internal StacksModel StudyMenuView(List<StacksModel> stacks)
    {
        AnsiConsole.Clear();

        var selection = AnsiConsole.Prompt(new SelectionPrompt<StacksModel>()
            .Title("Select a [green]stack to study[/]:")
            .UseConverter(stack => stack.Name)
            .AddChoices(stacks));

        return selection;
    }

    internal string StacksView(List<StacksModel> stacksModel)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Gray);

        table.AddColumn("Name", col => col.LeftAligned());

        foreach (var stack in stacksModel)
        {
            table.AddRow(stack.Name);
        }

        AnsiConsole.Write(table);

        return AnsiConsole.Ask<string>("Enter the [blue]name[/] of a stack you want to study or enter [blue]0[/] to go back:");
    }

    internal string StudyFlashcardView(FlashcardsDTO flashcard)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Gray);

        table.AddColumn("Question", col => col.LeftAligned());

        table.AddRow(flashcard.Question);

        AnsiConsole.Write(table);

        return AnsiConsole.Ask<string>("Type your answer or [blue]0[/] to quit:");
    }

    internal void StudyCompletedView(int score, int total)
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine($"[green]Study session completed![/]");
        AnsiConsole.MarkupLine($"Your score: [yellow]{score}[/] out of [yellow]{total}[/].");
        AnsiConsole.MarkupLine($"Press any key to continue...");
        Console.ReadKey();
    }

    internal void NoFlashcardsInStackView()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[red]No flashcards found in this stack! Press any key to continue...[/]");
        Console.ReadKey();
    }

    internal void IsCorrectAnswerView(bool isCorrect, FlashcardsDTO flashcard)
    {
        if (isCorrect)
            AnsiConsole.MarkupLine($"[green]Correct![/]");
        else
            AnsiConsole.MarkupLine($"[red]Incorrect! The correct answer was: [yellow]{flashcard.Answer}[/][/]");
        AnsiConsole.MarkupLine($"Press any key to continue...");
        Console.ReadKey();
    }

    internal string GetStackNameInput(List<StacksModel> stacksModel)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Gray);

        table.AddColumn("Name", col => col.LeftAligned());

        foreach (var stack in stacksModel)
        {
            table.AddRow(stack.Name);
        }

        AnsiConsole.Write(table);

        return AnsiConsole.Ask<string>("Enter the [blue]name[/] of a stack to see its study sessions, [blue]1[/] to see all study sessions or [blue]0[/] to go back:");
    }

    internal void ViewStudySessions(List<StudySessionsDTO> studySessions)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Gray);

        table.AddColumn("Id", col => col.LeftAligned());
        table.AddColumn("Score", col => col.LeftAligned());
        table.AddColumn("Date and Time", col => col.LeftAligned());
        table.AddColumn("Stack Name", col => col.LeftAligned());

        foreach (var session in studySessions)
        {
            table.AddRow(session.Id.ToString(), session.Score.ToString(), session.SessionDate.ToString("g"), session.StackName);
        }
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine($"Press any key to continue...");
        Console.ReadKey();
    }
}
