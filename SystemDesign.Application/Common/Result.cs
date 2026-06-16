namespace SystemDesign.Application.Common;

public enum ResultErrorType
{
    None = 0,
    Failure,
    NotFound
}

public class Result
{
    protected Result(bool isSuccess, string? error, ResultErrorType errorType)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorType = errorType;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string? Error { get; }

    public ResultErrorType ErrorType { get; }

    public static Result Success() => new(true, null, ResultErrorType.None);

    public static Result Failure(string error) => new(false, error, ResultErrorType.Failure);

    public static Result NotFound(string error) => new(false, error, ResultErrorType.NotFound);

    public static Result<T> Success<T>(T value) => new(value, true, null, ResultErrorType.None);

    public static Result<T> Failure<T>(string error) => new(default, false, error, ResultErrorType.Failure);

    public static Result<T> NotFound<T>(string error) => new(default, false, error, ResultErrorType.NotFound);
}

public sealed class Result<T> : Result
{
    private readonly T? _value;

    internal Result(T? value, bool isSuccess, string? error, ResultErrorType errorType)
        : base(isSuccess, error, errorType)
    {
        _value = value;
    }

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access the value of a failed result.");
}
