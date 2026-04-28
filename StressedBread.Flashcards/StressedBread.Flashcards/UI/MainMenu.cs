using Spectre.Console;
using static StressedBread.Flashcards.Enums;
using StressedBread.Flashcards.Converters;

namespace StressedBread.Flashcards.UI;

internal class MainMenu
{
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

                    break;
                case MenuOption.ManageFlashcards: 
                    break;
                case MenuOption.Exit:
                    return;
            }

        }
    }
}