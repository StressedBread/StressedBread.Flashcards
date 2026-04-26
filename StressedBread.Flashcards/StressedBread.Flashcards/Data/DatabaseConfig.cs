using Microsoft.Extensions.Configuration;

namespace StressedBread.Flashcards.Data;

internal class DatabaseConfig
{
    
    internal string DefaultConnectionString { get; }
    internal string FlashcardsConnectionString => DefaultConnectionString.Replace("master", "FlashcardsStressedBread");

    internal DatabaseConfig()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        DefaultConnectionString = configuration.GetConnectionString("DefaultConnection") 
                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
    }
}
