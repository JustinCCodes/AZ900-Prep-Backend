using AZ900Prep.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adds Services via Extension Methods
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configures Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSecurityHeaders(); // Custom Security Headers Middleware
app.UseHttpsRedirection(); // Enforces HTTPS
app.UseCors("FrontendOnly"); // Restricts to Frontend Origin
app.UseRateLimiter(); // Guards before hits endpoints

await app.UseSeedData();
app.Run();
