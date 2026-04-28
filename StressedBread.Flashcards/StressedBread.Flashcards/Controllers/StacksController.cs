using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.Data.Queries;
using StressedBread.Flashcards.Models;
using StressedBread.Flashcards.UI;
using static StressedBread.Flashcards.Enums;

namespace StressedBread.Flashcards.Controllers;

internal class StacksController
{
    private readonly StacksMenu _stacksMenu;
    private readonly DatabaseAccess _databaseAccess;
    private readonly StacksQueries _stacksQueries;

    private List<StacksModel> _stacks = new();

    public StacksController(StacksMenu stacksMenu, DatabaseAccess databaseAccess, StacksQueries stacksQueries)
    {
        _stacksMenu = stacksMenu;
        _databaseAccess = databaseAccess;
        _stacksQueries = stacksQueries;
    }

    internal void Menu()
    {
        while (true)
        {
            _stacks = _databaseAccess.Reader<StacksModel>(_stacksQueries.GetAllStacksQuery());
            string result = _stacksMenu.StacksMainMenuView(_stacks);

            if (result == "0")
                return;

            if (!_stacks.Any(s => s.Name.ToLower() == result.ToLower()))
                CreateStack(result);
            else
            {
                string stackName = _stacks.First(s => s.Name.ToLower() == result.ToLower()).Name;

                StackMenuOption option = _stacksMenu.ManageStackMenuView(stackName);

                if (option == StackMenuOption.ChangeStack)
                    continue;
                if (option == StackMenuOption.BackToMainMenu)
                    return;

                ManageStack(option, stackName);
            }
        }
    }

    internal void CreateStack(string name)
    {
        _databaseAccess.ExecuteQuery(_stacksQueries.CreateStackQuery(), new { Name = name });
    }

    internal void ManageStack(StackMenuOption option, string stackName)
    {
        switch (option)
        {
            case StackMenuOption.ViewFlashcards:
                break;
            case StackMenuOption.AddFlashcard:
                break;
            case StackMenuOption.EditFlashcard:
                break;
            case StackMenuOption.DeleteFlashcard:
                break;
            case StackMenuOption.DeleteStack:
                _databaseAccess.ExecuteQuery(_stacksQueries.DeleteStackQuery(), new { Name = stackName });
                break;
        }
    }
}
