namespace AZ900Prep.Api.Infrastructure;

// Extension methods for registering infrastructure services
public static class DependencyInjection
{
    // Configures and adds infrastructure services to the DI container
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // SQLite Database Setup
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Rate Limiting
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddFixedWindowLimiter("api-policy", opt =>
            {
                opt.Window = TimeSpan.FromMinutes(1);
                opt.PermitLimit = 60; // Max 60 requests per minute
                opt.QueueLimit = 0;   // Drop requests if limit is hit
            });
        });

        // Locked to Frontend Port
        services.AddCors(options =>
        {
            // CORS policy allowing only frontend origin
            options.AddPolicy("FrontendOnly", builder =>
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyMethod() // Allows all HTTP methods
                       .AllowAnyHeader()); // Allows all headers
        });

        services.AddScoped<GetStandardExamHandler>(); // Exam Handler Registration

        services.AddExceptionHandler<GlobalExceptionHandler>(); // Global Exception Handling

        services.AddProblemDetails(); // Problem Details Support

        // Returns the configured service collection
        return services;
    }

    // Seeds database with initial data
    public static async Task UseSeedData(this IApplicationBuilder app)
    {
        // Creates scope to get AppDbContext
        using var scope = app.ApplicationServices.CreateScope();
        // Gets AppDbContext instance
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Seeds database
        await DbInitializer.SeedAsync(context);
    }
}

