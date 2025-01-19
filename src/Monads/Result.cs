using System.Diagnostics.CodeAnalysis;
using SleepingBear.Functional.Errors;

namespace SleepingBear.Functional.Monads;

/// <summary>
///     Monad representing either a success or a failure.
/// </summary>
/// <typeparam name="T">The type of the lifted value.</typeparam>
[SuppressMessage(category: "Usage", checkId: "CA2225:Operator overloads have named alternates")]
public readonly record struct Result<T> where T : notnull
{
    private readonly Error? _error;
    private readonly T? _ok;

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
    ///     Default constructor.
    /// </summary>
    public Result()
    {
        this._ok = default;
        this._error = UnknownError.Value;
        this.IsOk = false;
    }

    /// <summary>
    ///     Flag indicating the monad contains a value.
    /// </summary>
    public bool IsOk { get; }

    /// <summary>
    ///     Flag indicating the monad contains an error.
    /// </summary>
    public bool IsError => !this.IsOk;

    /// <summary>
    ///     Deconstructs the monad.
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

    /// <summary>
    ///     Implicit operator for lifting a value to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="ok"></param>
    /// <returns></returns>
    public static implicit operator Result<T>(T ok)
    {
        return new Result<T>(ok);
    }

    /// <summary>
    ///     Implicit operator for lifting an <see cref="Error" /> to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }
}