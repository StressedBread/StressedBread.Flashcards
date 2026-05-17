using StressedBread.Flashcards.Controllers;
using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.Data.Queries;
using StressedBread.Flashcards.UI;

//Database configuration and initialization
var databaseConfig = new DatabaseConfig();
var startupView = new StartupView();
var databaseInitQueries = new DatabaseInitQueries();
var stacksQueries = new StacksQueries();
var flashcardsQueries = new FlashcardsQueries();
var defaultDatabaseAccess = new DatabaseAccess(databaseConfig.DefaultConnectionString);
var flashcardsDatabaseAccess = new DatabaseAccess(databaseConfig.FlashcardsConnectionString);
var databaseInitialization = new DatabaseInitializer(databaseConfig.DefaultConnectionString, databaseConfig.FlashcardsConnectionString, databaseInitQueries, defaultDatabaseAccess, flashcardsDatabaseAccess);

//Controllers and Menus
var stacksMenu = new StacksMenu();
var studyMenu = new StudyMenu();
var flashcardsUI = new FlashcardsUI();
var flashcardsController = new FlashcardsController(flashcardsUI, flashcardsDatabaseAccess, flashcardsQueries);
var stacksController = new StacksController(stacksMenu, flashcardsDatabaseAccess, stacksQueries, flashcardsController);
var studyController = new StudyController(flashcardsDatabaseAccess, stacksQueries, flashcardsQueries, studyMenu);
var mainMenu = new MainMenu(stacksController, flashcardsController, studyController);
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