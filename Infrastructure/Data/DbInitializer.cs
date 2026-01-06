namespace AZ900Prep.Api.Infrastructure.Data;

// Initializes and seeds the database
public static class DbInitializer
{
    // Seeds the database with initial data if empty
    public static async Task SeedAsync(AppDbContext context)
    {
        // Ensures database is up to date with migrations
        await context.Database.MigrateAsync();

        // Only seeds if database is empty
        if (await context.Questions.AnyAsync()) return;

        // Path to JSON seed file
        var seedPath = Path.Combine(AppContext.BaseDirectory, "Infrastructure", "Data", "questions-seed.json");
        // If the seed file doesn't exist, exits
        if (!File.Exists(seedPath)) return;

        // Reads and deserializes the JSON data
        var json = await File.ReadAllTextAsync(seedPath);
        var questions = JsonSerializer.Deserialize<List<Question>>(json, new JsonSerializerOptions
        {
            // Allows case insensitive property matching
            PropertyNameCaseInsensitive = true
        });

        // Adds questions to database if any were found
        if (questions is { Count: > 0 })
        {
            // Adds questions and saves changes
            await context.Questions.AddRangeAsync(questions);
            await context.SaveChangesAsync();
        }
    }
}
