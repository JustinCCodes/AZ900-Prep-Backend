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
            options.AddPolicy("FrontendOnly", builder =>
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });

        return services;
    }
}
