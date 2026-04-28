using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.UI;

var databaseConfig = new DatabaseConfig();
var startupView = new StartupView();
var databaseInitialization = new DatabaseInitializer(databaseConfig.DefaultConnectionString, databaseConfig.FlashcardsConnectionString);
var mainMenu = new MainMenu();

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