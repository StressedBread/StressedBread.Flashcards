using Spectre.Console;
using StressedBread.Flashcards.Converters;
using StressedBread.Flashcards.Models;
using static StressedBread.Flashcards.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StressedBread.Flashcards.UI;
internal class ReportsMenu
{
    internal ReportMenuOption ReportsMainMenuView()
    {
        AnsiConsole.Clear();
    
        return AnsiConsole.Prompt(new SelectionPrompt<ReportMenuOption>()
                .Title("Select a report to view:")
                .UseConverter(option => EnumToStringFormatAndConvert.Convert(option))
                .AddChoices(Enum.GetValues<ReportMenuOption>()));
    }

    internal void ReportView(ReportModel reportModel, string year)
    {
        AnsiConsole.Clear();
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Gray)
            .Title($"Sessions Per Month Per Stack for year: {year}");

        table.AddColumn("Stack Name", col => col.LeftAligned());

        foreach (var month in reportModel.MonthlyValues.Keys)
        {
            table.AddColumn(month);
        }

        table.AddRow(new [] { reportModel.StackName }
                .Concat(reportModel.MonthlyValues.Values.Select(v => v.ToString()))
                .ToArray());

        AnsiConsole.Write(table);
        Console.ReadKey(true);
    }

    internal string StacksViewReport(List<StacksModel> stacksModel)
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

        return AnsiConsole.Ask<string>("Enter the [blue]name[/] of a stack you want to view report of or enter [blue]0[/] to go back:");
    }

    internal string YearToFilter()
    {
        AnsiConsole.Clear();
        return AnsiConsole.Prompt(
            new TextPrompt<int>("Enter the year to filter by (e.g., 2024):")
                .Validate(input =>
                
                    input >= 1900 && input <= 2100 ?
                        ValidationResult.Success()
                    :
                        ValidationResult.Error("[red]Please enter a valid year.[/]")
                )).ToString();
    }

    internal void InvalidReport()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[red]Invalid selection. Please try again.[/]");
        AnsiConsole.MarkupLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}
