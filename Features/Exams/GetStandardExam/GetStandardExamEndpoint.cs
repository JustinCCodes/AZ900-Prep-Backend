namespace AZ900Prep.Api.Features.Exams.GetStandardExam;

// Endpoint mapping for retrieving standard exam
public static class GetStandardExamEndpoint
{
    // Maps GET /api/exams/standard endpoint
    public static void MapGetStandardExam(this IEndpointRouteBuilder app)
    {
        // Defines endpoint with rate limiting
        app.MapGet("/api/exams/standard", async (GetStandardExamHandler handler, CancellationToken ct) =>
        {
            // Calls handler to process request
            var result = await handler.HandleAsync(ct);
            return result.ToActionResult(); // Converts result to HTTP action result
        })
        // Adds rate limiting policy
        .RequireRateLimiting("api-policy");
    }
}
