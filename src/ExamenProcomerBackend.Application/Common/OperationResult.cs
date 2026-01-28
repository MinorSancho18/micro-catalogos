namespace ExamenProcomerBackend.Application.Common;

public sealed record OperationResult(bool Success, string? Error)
{
    public static OperationResult Ok() => new(true, null);
    public static OperationResult Fail(string error) => new(false, error);
}

public sealed record OperationResult<T>(bool Success, T? Data, string? Error)
{
    public static OperationResult<T> Ok(T data) => new(true, data, null);
    public static OperationResult<T> Fail(string error) => new(false, default, error);
}
