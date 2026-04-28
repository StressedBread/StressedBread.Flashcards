using StressedBread.Flashcards.Controllers;
using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.UI;

//Database configuration and initialization
var databaseConfig = new DatabaseConfig();
var startupView = new StartupView();
var databaseInitialization = new DatabaseInitializer(databaseConfig.DefaultConnectionString, databaseConfig.FlashcardsConnectionString);

//Controllers and Menus
var stacksMenu = new StacksMenu();
var stacksController = new StacksController(stacksMenu);
var mainMenu = new MainMenu(stacksController);
var databaseAccess = new DatabaseAccess(databaseConfig.FlashcardsConnectionString);

// Application flow
var isStringValid = databaseInitialization.IsDefaultConnectionStringValid();
var isDatabaseInitialized = databaseInitialization.InitializeDatabase();
var tableCreationResult = databaseInitialization.CreateTables();

startupView.ShowInitializationMessage();
startupView.ShowDatabaseStringValidation(isStringValid);
startupView.ShowDatabaseInitializationResult(isDatabaseInitialized);
startupView.ShowTableCreationResult(tableCreationResult);

if (isStringValid.IsSuccessful && isDatabaseInitialized.IsSuccessful && tableCreationResult.IsSuccessful)
{
    startupView.ContinueToMainMenu();
    mainMenu.Menu();
}
else
    startupView.ShowErrorAndExit();