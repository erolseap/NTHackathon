using System.Diagnostics.CodeAnalysis;

namespace NTHackathon.Application.Models;

public class Result
{
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }
    public string? Error { get; }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);

    private Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
}

public class Result<TValue>
{
    [MemberNotNullWhen(false, nameof(Error))]
    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess { get; }
    public TValue? Value { get; }
    public string? Error { get; }

    public static Result<TValue> Success(TValue value) => new(true, value, null);
    public static Result<TValue> Failure(string error) => new(false, default, error);

    private Result(bool isSuccess, TValue? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }
}
