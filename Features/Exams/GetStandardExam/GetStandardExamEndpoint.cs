namespace AZ900Prep.Api.Features.Exams.GetStandardExam;

// Endpoint mapping for retrieving a standard exam
public static class GetStandardExamEndpoint
{
    // Maps the GET /api/exams/standard endpoint
    public static void MapGetStandardExam(this IEndpointRouteBuilder app)
    {
        // Defines the endpoint handler
        app.MapGet("/api/exams/standard", async Task<Results<Ok<GetStandardExamResponse>, NotFound<string>>>
            (GetStandardExamHandler handler, CancellationToken ct) =>
        {
            // Invokes the handler to get exam
            var result = await handler.HandleAsync(ct);

            // Returns appropriate HTTP response based on result
            return result.IsSuccess
                ? TypedResults.Ok(result.Value!) // Success case
                : TypedResults.NotFound(result.Error!); // Failure case
        })
        // Applies rate limiting policy
        .RequireRateLimiting("api-policy");
    }
}
