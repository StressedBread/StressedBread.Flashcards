using StressedBread.Flashcards.Models;
using StressedBread.Flashcards.UI;

namespace StressedBread.Flashcards.Controllers;

internal class StacksController
{
    private readonly StacksMenu _stacksMenu;

    public StacksController(StacksMenu stacksMenu)
    {
        _stacksMenu = stacksMenu;
    }

    internal void Menu()
    {
        var stacks = new List<StacksModel>
        {
            new StacksModel { Id = 1, Name = "Spanish" },
            new StacksModel { Id = 2, Name = "Geography" },
            new StacksModel { Id = 3, Name = "Programming" }
        };

        while (true)
        {
            string result = _stacksMenu.StacksMenuView(stacks);

            if (result == "0")
                return;

            if (!stacks.Any(s => s.Name.ToLower() == result.ToLower()))
                CreateStack(result);
            else
                ManageStack(stacks.First(s => s.Name.ToLower() == result.ToLower()));
        }
    }

    internal void CreateStack(string name)
    {
        
    }

    internal void ManageStack(StacksModel stack)
    {
        // Logic to manage an existing stack
    }
}
