namespace AZ900Prep.Api.Common;

// Represents an error in a result
public record ResultError(string Code, string Description);

// Generic result wrapper indicating success or failure
public class Result<T>
{
    public T? Value { get; } // May be null in failure cases
    public bool IsSuccess { get; } // True if operation succeeded
    public ResultError? Error { get; } // May be null in success cases

    // Protected constructor to enforce factory method usage
    protected Result(T? value, bool isSuccess, ResultError? error)
    {
        Value = value; // May be null in failure cases
        IsSuccess = isSuccess; // True if operation succeeded
        Error = error; // May be null in success cases
    }

    // Factory method for success result
    public static Result<T> Success(T value) => new(value, true, null);
    public static Result<T> Failure(ResultError error) => new(default, false, error);

    // Factory method for NotFound error
    public static Result<T> NotFound(string description = "Resource not found")
        => Failure(new ResultError("Error.NotFound", description));
}
