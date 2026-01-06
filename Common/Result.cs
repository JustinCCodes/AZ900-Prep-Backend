namespace AZ900Prep.Api.Common;

// Generic result type for operations
public record Result<T>(T? Value, bool IsSuccess, string? Error = null)
{
    // Factory methods for success and failure
    public static Result<T> Success(T value) => new(value, true);
    public static Result<T> Failure(string error) => new(default, false, error);
}
