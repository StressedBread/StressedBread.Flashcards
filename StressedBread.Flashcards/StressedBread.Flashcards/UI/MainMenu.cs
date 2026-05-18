using Spectre.Console;
using StressedBread.Flashcards.Controllers;
using StressedBread.Flashcards.Converters;
using static StressedBread.Flashcards.Enums;

namespace StressedBread.Flashcards.UI;

internal class MainMenu
{
    private readonly StacksController _stacksController;
    private readonly FlashcardsController _flashcardsController;
    private readonly StudyController _studyController;
    private readonly ReportsController _reportsController;
    internal MainMenu(StacksController stacksController, FlashcardsController flashcardsController, StudyController studyController, ReportsController reportsController)
    {
        _stacksController = stacksController;
        _flashcardsController = flashcardsController;
        _studyController = studyController;
        _reportsController = reportsController;
    }

    internal void Menu()
    {
        while (true)
        {
            AnsiConsole.Clear();

            var selection = AnsiConsole.Prompt(new SelectionPrompt<MenuOption>()
                .Title("Welcome to [green]Flashcards[/]! Please select an option:")
                .UseConverter(option => EnumToStringFormatAndConvert.Convert(option))
                .AddChoices(Enum.GetValues<MenuOption>()));

            switch (selection)
            {
                case MenuOption.ManageStacks:
                    _stacksController.SelectStack();
                    break;
                case MenuOption.ManageFlashcards:
                    _flashcardsController.FlashcardsMenu();
                    break;
                case MenuOption.Study:
                    _studyController.StudyStackSelection();
                    break;
                case MenuOption.ViewStudySessions:
                    _studyController.ViewStudySessions();
                    break;
                case MenuOption.ViewReports:
                    _reportsController.ViewReports();
                    break;
                case MenuOption.Exit:
                    return;
            }
        }
    }
}