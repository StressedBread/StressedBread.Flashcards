using Spectre.Console;
using StressedBread.Flashcards.Controllers;
using StressedBread.Flashcards.Converters;
using static StressedBread.Flashcards.Enums;

namespace StressedBread.Flashcards.UI;

internal class MainMenu
{
    private readonly StacksController _stacksController;
    internal MainMenu(StacksController stacksController)
    {
        _stacksController = stacksController;
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
                    _stacksController.Menu();
                    break;
                case MenuOption.ManageFlashcards:
                    break;
                case MenuOption.Exit:
                    return;
            }
        }
    }
}