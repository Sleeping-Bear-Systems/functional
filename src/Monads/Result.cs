using System.Diagnostics.CodeAnalysis;
using SleepingBear.Functional.Errors;

namespace SleepingBear.Functional.Monads;

/// <summary>
/// Monad representing either a success or a failure.
/// </summary>
/// <typeparam name="T">The type of the lifted value.</typeparam>
[SuppressMessage("Usage", "CA2225:Operator overloads have named alternates")]
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

    /// <summary>
    /// Implicit operator for lifting a value to a <see cref="Result{T}"/>. 
    /// </summary>
    /// <param name="ok"></param>
    /// <returns></returns>
    public static implicit operator Result<T>(T ok)
    {
        return new Result<T>(ok);
    }

    /// <summary>
    /// Implicit operator for lifting an <see cref="Error"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }
}

/// <summary>
/// Extension methods for <see cref="Result{T}"/>.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
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
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResult<T>(this Error error) where T : notnull
    {
        return new Result<T>(error);
    }

    /// <summary>
    /// Maps a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/>.
    /// </summary>
    /// <param name="result">The result being mapped.</param>
    /// <param name="mapFunc">The mapping function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Result{TOut}"/>.</returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapFunc)
        where TIn : notnull
        where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(mapFunc);

        return result switch
        {
            (true, var ok, _) => mapFunc(ok!).ToResult(),
            (false, _, var error) => error!.ToResult<TOut>()
        };
    }

    /// <summary>
    /// Maps a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/> asynchronously.
    /// </summary>
    /// <param name="task">The task containing the result being mapped.</param>
    /// <param name="mapFunc">The mapping function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Task{TResult}"/> containing <see cref="Result{TOut}"/>.</returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> task, Func<TIn, TOut> mapFunc)
        where TIn : notnull
        where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapFunc);

        var result = await task.ConfigureAwait(false);
        return result switch
        {
            (true, var ok, _) => mapFunc(ok!).ToResult(),
            (false, _, var error) => error!.ToResult<TOut>()
        };
    }

    /// <summary>
    /// Maps a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/> asynchronously.
    /// </summary>
    /// <param name="task">The task containing the result being mapped.</param>
    /// <param name="mapFuncAsync">The mapping function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Task{TResult}"/> containing <see cref="Result{TOut}"/>.</returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> Map<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Task<TOut>> mapFuncAsync)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapFuncAsync);

        var result = await task.ConfigureAwait(false);
        return result switch
        {
            (true, var ok, _) => new Result<TOut>(await mapFuncAsync(ok!).ConfigureAwait(false)),
            (false, _, var error) => new Result<TOut>(error!)
        };
    }

    /// <summary>
    /// Binds a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="bindFunc"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bindFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(bindFunc);

        return result switch
        {
            (true, var ok, _) => bindFunc(ok!),
            (false, _, var error) => error!.ToResult<TOut>()
        };
    }

    /// <summary>
    /// Binds a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/> asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="bindFunc"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> Bind<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Result<TOut>> bindFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFunc);

        var result = await task.ConfigureAwait(false);
        return result switch
        {
            (true, var ok, _) => bindFunc(ok!),
            (false, _, var error) => error!.ToResult<TOut>()
        };
    }

    /// <summary>
    /// Binds a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/> asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="bindFunc"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> Bind<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Task<Result<TOut>>> bindFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFunc);

        var result = await task.ConfigureAwait(false);
        return result switch
        {
            (true, var ok, _) => await bindFunc(ok!).ConfigureAwait(false),
            (false, _, var error) => error!.ToResult<TOut>()
        };
    }

    /// <summary>
    /// Matches a <see cref="Result{TIn}"/> to a value.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="okFunc"></param>
    /// <param name="errorFunc"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static TOut Match<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> okFunc, Func<Error, TOut> errorFunc)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(okFunc);
        ArgumentNullException.ThrowIfNull(errorFunc);

        return result switch
        {
            (true, var ok, _) => okFunc(ok!),
            (false, _, var error) => errorFunc(error!)
        };
    }

    /// <summary>
    /// Matches a <see cref="Result{TIn}"/> to a value asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="okFunc"></param>
    /// <param name="errorFunc"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchAsync<TIn, TOut>(this Task<Result<TIn>> task, Func<TIn, TOut> okFunc,
        Func<Error, TOut> errorFunc)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFunc);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var result = await task.ConfigureAwait(false);
        return result switch
        {
            (true, var ok, _) => okFunc(ok!),
            (false, _, var error) => errorFunc(error!)
        };
    }

    /// <summary>
    /// Matches a <see cref="Result{TIn}"/> to a value asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="okFunc"></param>
    /// <param name="errorFunc"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Task<TOut>> okFunc,
        Func<Error, Task<TOut>> errorFunc)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFunc);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var result = await task.ConfigureAwait(false);
        return result switch
        {
            (true, var ok, _) => await okFunc(ok!).ConfigureAwait(false),
            (false, _, var error) => await errorFunc(error!).ConfigureAwait(false)
        };
    }

    /// <summary>
    /// Taps a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="okAction"></param>
    /// <param name="errorAction"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static Result<T> Tap<T>(
        this Result<T> result,
        Action<T> okAction,
        Action<Error> errorAction) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(okAction);
        ArgumentNullException.ThrowIfNull(errorAction);

        var (isOk, ok, error) = result;
        if (isOk)
        {
            okAction(ok!);
        }
        else
        {
            errorAction(error!);
        }

        return result;
    }

    /// <summary>
    /// Tap a <see cref="Result{T}"/> asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="okAction"></param>
    /// <param name="errorAction"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> TapAsync<T>(
        this Task<Result<T>> task,
        Action<T> okAction,
        Action<Error> errorAction) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okAction);
        ArgumentNullException.ThrowIfNull(errorAction);

        var result = await task.ConfigureAwait(false);
        var (isOk, ok, error) = result;
        if (isOk)
        {
            okAction(ok!);
        }
        else
        {
            errorAction(error!);
        }

        return result;
    }

    /// <summary>
    /// Taps a <see cref="Result{T}"/> asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="okFunc"></param>
    /// <param name="errorFunc"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> TapAsync<T>(
        this Task<Result<T>> task,
        Func<T, Task> okFunc,
        Func<Error, Task> errorFunc) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFunc);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var result = await task.ConfigureAwait(false);
        var (isOk, ok, error) = result;
        if (isOk)
        {
            await okFunc(ok!).ConfigureAwait(false);
        }
        else
        {
            await errorFunc(error!).ConfigureAwait(false);
        }

        return result;
    }
}
