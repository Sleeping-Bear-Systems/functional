using SleepingBear.Functional.Errors;

namespace SleepingBear.Functional.Monads;

/// <summary>
/// Monad representing either a success or a failure.
/// </summary>
/// <typeparam name="T">The type of the lifted value.</typeparam>
public readonly record struct Result<T> where T : notnull
{
    private readonly T? _ok;

    private readonly Error? _error;

    internal Result(T ok)
    {
        this._ok = ok;
        this._error = null;
        this.IsOk = true;
    }

    internal Result(Error error)
    {
        this._ok = default;
        this._error = error;
        this.IsOk = false;
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Result()
    {
        this._ok = default;
        this._error = new UnknownError();
        this.IsOk = false;
    }

    /// <summary>
    /// Flag indicating the monad contains a value.
    /// </summary>
    public bool IsOk { get; }

    /// <summary>
    /// Flag indicating the monad contains an error.
    /// </summary>
    public bool IsError => !this.IsOk;

    /// <summary>
    /// Deconstructs the monad.
    /// </summary>
    /// <param name="isOk">The flag indicating the monad contains a value.</param>
    /// <param name="ok">The OK value.</param>
    /// <param name="error">The error.</param>
    public void Deconstruct(out bool isOk, out T? ok, out Error? error)
    {
        isOk = this.IsOk;
        ok = this._ok;
        error = this._error;
    }
}

/// <summary>
/// Extension methods for <see cref="Result{T}"/>.
/// </summary>
public static class Result
{
    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="ok">The value being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResult<T>(this T ok) where T : notnull
    {
        return new Result<T>(ok);
    }

    /// <summary>
    /// Lifts a <see cref="Error"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="error">The error being lifted.</param>
    /// <typeparam name="T">THe type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResult<T>(this Error error) where T : notnull
    {
        return new Result<T>(error);
    }
}
