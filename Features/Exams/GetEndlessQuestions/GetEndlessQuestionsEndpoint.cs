namespace AZ900Prep.Api.Features.Exams.GetEndlessQuestions;

// Endpoint mapping for retrieving endless random questions
public static class GetEndlessQuestionsEndpoint
{
    // Maps GET /api/exams/endless endpoint
    public static void MapGetEndlessQuestions(this IEndpointRouteBuilder app)
    {
        // Defines endpoint with rate limiting
        app.MapGet("/api/exams/endless", async (GetEndlessQuestionsHandler handler, CancellationToken ct) =>
        {
            // Calls handler to process request
            var result = await handler.HandleAsync(10, ct);
            // Converts result to HTTP action result
            return result.ToActionResult();
        })
        // Adds rate limiting policy
        .RequireRateLimiting("api-policy");
    }
}
