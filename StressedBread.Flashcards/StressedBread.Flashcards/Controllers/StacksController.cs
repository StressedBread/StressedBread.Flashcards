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
    private readonly FlashcardsController _flashcardsController;

    private List<StacksModel> _stacks = new();

    public StacksController(StacksMenu stacksMenu, DatabaseAccess databaseAccess, StacksQueries stacksQueries, FlashcardsController flashcardsController)
    {
        _stacksMenu = stacksMenu;
        _databaseAccess = databaseAccess;
        _stacksQueries = stacksQueries;
        _flashcardsController = flashcardsController;
    }

    internal void Menu()
    {
        while (true)
        {
            _stacks = _databaseAccess.Reader<StacksModel>(_stacksQueries.GetAllStacksQuery());
            string result = _stacksMenu.StacksMainMenuView(_stacks);

            if (String.Equals(result.Trim(), "0", StringComparison.OrdinalIgnoreCase))
                return;

            if (!_stacks.Any(s => s.Name.ToLower() == result.ToLower()))
                CreateStack(result);
            else
            {
                string stackName = _stacks.First(s => s.Name.ToLower() == result.ToLower()).Name;
                int stackId = _stacks.First(s => s.Name.ToLower() == result.ToLower()).Id;

                StackMenuOption option = _stacksMenu.ManageStackMenuView(stackName);

                if (option == StackMenuOption.ChangeStack)
                    continue;
                if (option == StackMenuOption.BackToMainMenu)
                    return;

                ManageStack(option, stackId);
            }
        }
    }

    internal void CreateStack(string name)
    {
        _databaseAccess.ExecuteQuery(_stacksQueries.CreateStackQuery(), new { Name = name });
    }

    internal void ManageStack(StackMenuOption option, int stackId)
    {
        switch (option)
        {
            case StackMenuOption.ViewFlashcards:
                _flashcardsController.ViewFlashcards(stackId);
                break;
            case StackMenuOption.AddFlashcard:
                _flashcardsController.AddFlashcard(stackId);
                break;
            case StackMenuOption.EditFlashcard:
                _flashcardsController.EditFlashcard(stackId);
                break;
            case StackMenuOption.DeleteFlashcard:
                _flashcardsController.DeleteFlashcard(stackId);
                break;
            case StackMenuOption.DeleteStack:
                _databaseAccess.ExecuteQuery(_stacksQueries.DeleteStackQuery(), new { Id = stackId });
                break;
        }
    }
}
