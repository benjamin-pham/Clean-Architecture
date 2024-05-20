using Application.Exceptions;

namespace Application.Libraries;
public class Result
{
    public Result() { }

    protected internal Result(bool isSuccess, string msg)
    {
        if (!isSuccess)
        {
            throw new AppException(msg);
        }

        IsSuccess = isSuccess;
        Message = msg;
    }

    public bool IsSuccess { get; }
    public string Message { get; }

    public static Result Success() => new(true, string.Empty);
    public static Result Success(string msg) => new(true, msg);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, string.Empty);
    public static Result<TValue> Success<TValue>(TValue value, string msg) =>
        new(value, true, msg);

    public static Result Failure(string msg) =>
        new(false, msg);
    public static Result Failure() =>
        new(false, "Fail");

    public static Result<TValue> Failure<TValue>(string msg) =>
        new(default, false, msg);
}
public class Result<TData> : Result
{
    protected internal Result(TData value, bool isSuccess, string msg)
       : base(isSuccess, msg) =>
       Data = value;
    public TData Data { get; }
}

