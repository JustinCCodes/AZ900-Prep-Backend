namespace AZ900Prep.Api.Common;

// Extension methods for converting Result<T> to HTTP action results
public static class ResultExtensions
{
    // Converts a Result<T> to appropriate HTTP action results
    public static Results<Ok<T>, NotFound<ResultError>, BadRequest<ResultError>, Conflict<ResultError>> ToActionResult<T>(this Result<T> result)
    {
        // Checks if result indicates success
        if (result.IsSuccess)
        {
            // Returns OK with result value
            return TypedResults.Ok(result.Value!);
        }

        // Maps specific error codes to HTTP results
        return result.Error?.Code switch
        {
            "Error.NotFound" => TypedResults.NotFound(result.Error), // NotFound for not found errors
            "Error.Validation" => TypedResults.BadRequest(result.Error), // BadRequest for validation errors
            "Error.Conflict" => TypedResults.Conflict(result.Error), // Conflict for conflict errors
            _ => TypedResults.BadRequest(result.Error!) // Default fallback
        };
    }
}
