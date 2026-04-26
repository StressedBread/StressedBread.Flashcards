using Spectre.Console;

namespace StressedBread.Flashcards.UI;

internal class StartupView
{
    internal void ShowDatabaseStringValidation(bool isValid)
    {
        if (!isValid)
            AnsiConsole.MarkupLine("[red]Error:[/] Connection string is null or empty..");
        else
            AnsiConsole.MarkupLine("[green]Connection string is valid.[/]");
    }

    internal void ShowDatabaseInitializationResult(bool isInitialized)
    {
        if (isInitialized)
            AnsiConsole.MarkupLine("[green]Database already exists.[/]");
        else
            AnsiConsole.MarkupLine("[yellow]Database does not exist. It will be created.[/]");
    }

    internal void ShowTableCreationResult((bool Stacks, bool Flashcards) result)
    {
        if (result.Stacks)
            AnsiConsole.MarkupLine("[yellow]Stacks table already exists.[/]");
        else
            AnsiConsole.MarkupLine("[green]Stacks table created successfully.[/]");

        if (result.Flashcards)
            AnsiConsole.MarkupLine("[yellow]Flashcards table already exists.[/]");
        else
            AnsiConsole.MarkupLine("[green]Flashcards table created successfully.[/]");        
    }

    internal void ContinueToMainMenu()
    {
        AnsiConsole.MarkupLine("\n[blue]Press any key to continue to the main menu...[/]");
        Console.ReadKey(true);
    }
}
