namespace AZ900Prep.Api.Infrastructure.Data;

// Database context for the application
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // DbSets representing the tables in the database
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();

    // Configures the model relationships and constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensures cascading deletes are handled correctly
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne()
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
