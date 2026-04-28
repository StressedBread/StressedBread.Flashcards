using Spectre.Console;
using StressedBread.Flashcards.Models;

namespace StressedBread.Flashcards.UI;
internal class StacksMenu
{
    internal string StacksMenuView(List<StacksModel> stacksModel)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Gray)
            .Title("[blue bold]Stacks Menu[/]");

        table.AddColumn("Id", col => col.LeftAligned());
        table.AddColumn("Name", col => col.LeftAligned());

        foreach (var stack in stacksModel)
        {
            table.AddRow(stack.Id.ToString(), stack.Name);
        }

        AnsiConsole.Write(table);

        return AnsiConsole.Ask<string>("Enter the [blue]name[/] of a stack to manage or create the stack or enter [blue]0[/] to go back:");
    }
}
