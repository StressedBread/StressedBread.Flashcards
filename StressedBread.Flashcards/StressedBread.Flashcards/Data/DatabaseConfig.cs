using Microsoft.Extensions.Configuration;

namespace StressedBread.Flashcards.Data;

internal class DatabaseConfig
{

    internal string DefaultConnectionString { get; }
    internal string FlashcardsConnectionString { get; }

    internal DatabaseConfig()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        DefaultConnectionString = configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");

        FlashcardsConnectionString = configuration.GetConnectionString("StressedBreadFlashcards")
                                   ?? throw new InvalidOperationException("Connection string 'StressedBreadFlashcards' not found in appsettings.json.");
    }
}
