using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.UI;

var databaseConfig = new DatabaseConfig();
var startupView = new StartupView();
var databaseInitialization = new DatabaseInitializer(databaseConfig.DefaultConnectionString, databaseConfig.FlashcardsConnectionString);
var mainMenu = new MainMenu();

startupView.ShowDatabaseStringValidation(databaseInitialization.IsDefaultConnectionStringValid());
startupView.ShowDatabaseInitializationResult(databaseInitialization.InitializeDatabase());
startupView.ShowTableCreationResult(databaseInitialization.CreateTables());
startupView.ContinueToMainMenu();

mainMenu.Menu();