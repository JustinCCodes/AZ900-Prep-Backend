using AZ900Prep.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adds Services via Extension Methods
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configures Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseSecurityHeaders(); // Custom Security Headers Middleware
app.UseExceptionHandler(); // Global Exception Handling Middleware
app.UseHttpsRedirection(); // Enforces HTTPS
app.UseCors("FrontendOnly"); // Restricts to Frontend Origin
app.UseRateLimiter(); // Guards before hits endpoints

app.MapGetStandardExam(); // Maps Exam Endpoint

await app.UseSeedData(); // Seeds Initial Data
app.Run(); // Starts the Application
