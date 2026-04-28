using Spectre.Console;
using StressedBread.Flashcards.Models;

namespace StressedBread.Flashcards.UI;

internal class StartupView
{
    internal void ShowInitializationMessage()
    {
        AnsiConsole.MarkupLine("[green]Initializing database...[/]");
    }

    internal void ShowDatabaseStringValidation(DatabaseSetupResultModel result)
    {
        if (!result.IsSuccessful)
            AnsiConsole.MarkupLine("[red]Error:[/] Connection strings are null or empty.");
    }

    internal void ShowDatabaseInitializationResult(DatabaseSetupResultModel result)
    {
        if (!result.IsSuccessful)
            AnsiConsole.MarkupLine($"[red]Error: {result.ErrorMessage}[/]");
    }

    internal void ShowTableCreationResult(DatabaseSetupResultModel result)
    {
        if (!result.IsSuccessful)
            AnsiConsole.MarkupLine($"[red]Error: {result.ErrorMessage}[/]");
    }

    internal void ContinueToMainMenu()
    {
        AnsiConsole.MarkupLine("[green]Database initialized.[/]");
        AnsiConsole.MarkupLine("\n[blue]Press any key to continue to the main menu...[/]");
        Console.ReadKey(true);
    }

    internal void ShowErrorAndExit()
    {
        AnsiConsole.MarkupLine("\n[red]Initialization failed. Press any key to exit...[/]");
        Console.ReadKey(true);
    }
}
