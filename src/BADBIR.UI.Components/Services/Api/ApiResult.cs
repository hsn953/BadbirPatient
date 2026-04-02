namespace BADBIR.UI.Components.Services.Api;

/// <summary>
/// Represents the outcome of an API call — success or a user-readable error message.
/// </summary>
public class ApiResult
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }

    protected ApiResult(bool isSuccess, string? errorMessage)
    {
        IsSuccess    = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static ApiResult Success() => new(true, null);
    public static ApiResult Failure(string message) => new(false, message);
}

/// <summary>
/// Typed variant that also carries a data payload on success.
/// </summary>
public class ApiResult<T> : ApiResult
{
    public T? Data { get; }

    private ApiResult(bool isSuccess, T? data, string? errorMessage)
        : base(isSuccess, errorMessage)
        => Data = data;

    public static ApiResult<T> Success(T data) => new(true, data, null);
    public new static ApiResult<T> Failure(string message) => new(false, default, message);
}
