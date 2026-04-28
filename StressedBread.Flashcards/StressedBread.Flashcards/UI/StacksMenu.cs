using Spectre.Console;
using StressedBread.Flashcards.Converters;
using StressedBread.Flashcards.Models;
using static StressedBread.Flashcards.Enums;

namespace StressedBread.Flashcards.UI;
internal class StacksMenu
{
    internal string StacksMainMenuView(List<StacksModel> stacksModel)
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

        return AnsiConsole.Ask<string>("Enter the [blue]name[/] of a stack to manage or create the stack or enter [blue]0[/] to go back:");
    }

    internal StackMenuOption ManageStackMenuView(string stackName)
    {
        AnsiConsole.Clear();

        return AnsiConsole.Prompt(new SelectionPrompt<StackMenuOption>()
                .Title($"Select an option for the stack: [blue]{stackName}[/]")
                .UseConverter(option => EnumToStringFormatAndConvert.Convert(option))
                .AddChoices(Enum.GetValues<StackMenuOption>()));
    }
}
