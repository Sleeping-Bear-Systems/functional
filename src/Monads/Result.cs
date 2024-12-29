using System.Diagnostics.CodeAnalysis;
using SleepingBear.Functional.Errors;

namespace SleepingBear.Functional.Monads;

/// <summary>
/// Monad representing either a success or a failure.
/// </summary>
/// <typeparam name="T">The type of the lifted value.</typeparam>
[SuppressMessage(category: "Usage", checkId: "CA2225:Operator overloads have named alternates")]
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
        this._error = UnknownError.Value;
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
[SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
public static class Result
{
    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="ok">The value being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResultOk<T>(this T ok) where T : notnull
    {
        return new Result<T>(ok);
    }

    /// <summary>
    /// Lifts a <see cref="Error"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="error">The error being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResultError<T>(this Error error) where T : notnull
    {
        return new Result<T>(error);
    }

    /// <summary>
    /// Lifts a <see cref="UnknownError"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResultError<T>() where T : notnull
    {
        return new Result<T>(UnknownError.Value);
    }

    /// <summary>
    /// Lifts a value to <see cref="GenericError{T}"/> and then to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="value">The error value being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <typeparam name="TError">The tpe of the lifted error value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResultError<T, TError>(this TError value) where T : notnull where TError : notnull
    {
        return new Result<T>(value.ToGenericError());
    }

    /// <summary>
    /// Converts an exception to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="ex">The exception being lifted.</param>
    /// <typeparam name="T">The type of the value being lifted.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResultError<T>(this Exception ex) where T : notnull
    {
        return new ExceptionError(ex).ToResultError<T>();
    }

    /// <summary>
    /// Maps a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/>.
    /// </summary>
    /// <param name="result">The result being mapped.</param>
    /// <param name="mapFunc">The mapping function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Result{TOut}"/>.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapFunc)
        where TIn : notnull
        where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(mapFunc);

        return result switch
        {
            (true, var ok, _) => new Result<TOut>(mapFunc(ok!)),
            (false, _, var error) => new Result<TOut>(error!)
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
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> task, Func<TIn, TOut> mapFunc)
        where TIn : notnull
        where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, var ok, _) => new Result<TOut>(mapFunc(ok!)),
            (false, _, var error) => new Result<TOut>(error!)
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
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> Map<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Task<TOut>> mapFuncAsync)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapFuncAsync);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, var ok, _) => new Result<TOut>(await mapFuncAsync(ok!)
                .ConfigureAwait(continueOnCapturedContext: false)),
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
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bindFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(bindFunc);

        return result switch
        {
            (true, var ok, _) => bindFunc(ok!),
            (false, _, var error) => new Result<TOut>(error!)
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
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Result<TOut>> bindFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, var ok, _) => bindFunc(ok!),
            (false, _, var error) => new Result<TOut>(error!)
        };
    }

    /// <summary>
    /// Binds a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/> asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="bindFuncAsync"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Task<Result<TOut>>> bindFuncAsync)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFuncAsync);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, var ok, _) => await bindFuncAsync(ok!).ConfigureAwait(continueOnCapturedContext: false),
            (false, _, var error) => new Result<TOut>(error!)
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
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
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
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchAsync<TIn, TOut>(this Task<Result<TIn>> task, Func<TIn, TOut> okFunc,
        Func<Error, TOut> errorFunc)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFunc);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
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
    /// <param name="okFuncAsync"></param>
    /// <param name="errorFuncAsync"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Task<TOut>> okFuncAsync,
        Func<Error, Task<TOut>> errorFuncAsync)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFuncAsync);
        ArgumentNullException.ThrowIfNull(errorFuncAsync);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, var ok, _) => await okFuncAsync(ok!).ConfigureAwait(continueOnCapturedContext: false),
            (false, _, var error) => await errorFuncAsync(error!).ConfigureAwait(continueOnCapturedContext: false)
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
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
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
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> TapAsync<T>(
        this Task<Result<T>> task,
        Action<T> okAction,
        Action<Error> errorAction) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okAction);
        ArgumentNullException.ThrowIfNull(errorAction);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
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
    /// <param name="okFuncAsync"></param>
    /// <param name="errorFuncAsync"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> TapAsync<T>(
        this Task<Result<T>> task,
        Func<T, Task> okFuncAsync,
        Func<Error, Task> errorFuncAsync) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFuncAsync);
        ArgumentNullException.ThrowIfNull(errorFuncAsync);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, error) = result;
        if (isOk)
        {
            await okFuncAsync(ok!).ConfigureAwait(continueOnCapturedContext: false);
        }
        else
        {
            await errorFuncAsync(error!).ConfigureAwait(continueOnCapturedContext: false);
        }

        return result;
    }

    /// <summary>
    /// Binds a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="predicate"></param>
    /// <param name="bindFunc"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<T> BindIf<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        Func<T, Result<T>> bindFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(bindFunc);

        var (isOk, ok, _) = result;
        return isOk && predicate(ok!)
            ? bindFunc(ok!)
            : result;
    }

    /// <summary>
    /// Maps the error of a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="mapFunc"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<T> MapError<T>(
        this Result<T> result,
        Func<Error, Result<T>> mapFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(mapFunc);
        return result switch
        {
            (true, _, _) => result,
            (false, _, var error) => mapFunc(error!)
        };
    }

    /// <summary>
    /// Maps the error of a <see cref="Result{T}"/> asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="mapFunc"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> MapErrorAsync<T>(
        this Task<Result<T>> task,
        Func<Error, Result<T>> mapFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, _, _) => result,
            (false, _, var error) => mapFunc(error!)
        };
    }

    /// <summary>
    /// Maps the error of a <see cref="Result{T}"/> asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="mapFuncAsync"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> MapErrorAsync<T>(
        this Task<Result<T>> task,
        Func<Error, Task<Result<T>>> mapFuncAsync)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapFuncAsync);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, _, _) => result,
            (false, _, var error) => await mapFuncAsync(error!).ConfigureAwait(continueOnCapturedContext: false)
        };
    }

    /// <summary>
    /// Binds a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="bindFunc"></param>
    /// <param name="bindErrorFunc"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<TOut> Bind<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Result<TOut>> bindFunc,
        Func<Error, Result<TOut>> bindErrorFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(bindFunc);
        ArgumentNullException.ThrowIfNull(bindErrorFunc);

        return result switch
        {
            (true, var ok, _) => bindFunc(ok!),
            (false, _, var error) => bindErrorFunc(error!)
        };
    }

    /// <summary>
    /// Binds a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/>.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="bindFunc"></param>
    /// <param name="bindErrorFunc"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Result<TOut>> bindFunc,
        Func<Error, Result<TOut>> bindErrorFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFunc);
        ArgumentNullException.ThrowIfNull(bindErrorFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, var ok, _) => bindFunc(ok!),
            (false, _, var error) => bindErrorFunc(error!)
        };
    }

    /// <summary>
    /// Binds a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/>.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="bindFuncAsync"></param>
    /// <param name="bindErrorFuncAsync"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Task<Result<TOut>>> bindFuncAsync,
        Func<Error, Task<Result<TOut>>> bindErrorFuncAsync)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFuncAsync);
        ArgumentNullException.ThrowIfNull(bindErrorFuncAsync);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        return result switch
        {
            (true, var ok, _) => await bindFuncAsync(ok!).ConfigureAwait(continueOnCapturedContext: false),
            (false, _, var error) => await bindErrorFuncAsync(error!).ConfigureAwait(continueOnCapturedContext: false)
        };
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="value">The value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResultIf<T>(
        this T value,
        Func<T, bool> predicate,
        Func<T, Error> errorFunc) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(errorFunc);

        return predicate(value)
            ? new Result<T>(value)
            : new Result<T>(errorFunc(value));
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}"/> containing value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static async Task<Result<T>> ToResultIfAsync<T>(
        this Task<T> task,
        Func<T, bool> predicate,
        Func<T, Error> errorFunc) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return predicate(value)
            ? new Result<T>(value)
            : new Result<T>(errorFunc(value));
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}"/> containing value being lifted.</param>
    /// <param name="predicateAsync">The predicate.</param>
    /// <param name="errorFuncAsync">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static async Task<Result<T>> ToResultIfAsync<T>(
        this Task<T> task,
        Func<T, Task<bool>> predicateAsync,
        Func<T, Task<Error>> errorFuncAsync) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicateAsync);
        ArgumentNullException.ThrowIfNull(errorFuncAsync);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return await predicateAsync(value).ConfigureAwait(continueOnCapturedContext: false)
            ? new Result<T>(value)
            : new Result<T>(await errorFuncAsync(value).ConfigureAwait(continueOnCapturedContext: false));
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="value">The value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResultIf<T>(
        this T value,
        Func<T, bool> predicate,
        Error error) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(error);

        return predicate(value)
            ? new Result<T>(value)
            : new Result<T>(error);
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}"/> containing value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static async Task<Result<T>> ToResultIfAsync<T>(
        this Task<T> task,
        Func<T, bool> predicate,
        Error error) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicate);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return predicate(value)
            ? new Result<T>(value)
            : new Result<T>(error);
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}"/> containing value being lifted.</param>
    /// <param name="predicateAsync">The predicate.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static async Task<Result<T>> ToResultIfAsync<T>(
        this Task<T> task,
        Func<T, Task<bool>> predicateAsync,
        Error error) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicateAsync);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return await predicateAsync(value).ConfigureAwait(continueOnCapturedContext: false)
            ? new Result<T>(value)
            : new Result<T>(error);
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="value">The value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static Result<T> ToResultIf<T>(
        this T value,
        Func<T, bool> predicate) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return predicate(value)
            ? new Result<T>(value)
            : new Result<T>(UnknownError.Value);
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}"/> containing value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static async Task<Result<T>> ToResultIfAsync<T>(
        this Task<T> task,
        Func<T, bool> predicate) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicate);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return predicate(value)
            ? new Result<T>(value)
            : new Result<T>(UnknownError.Value);
    }

    /// <summary>
    /// Lifts a value to a <see cref="Result{T}"/> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}"/> containing value being lifted.</param>
    /// <param name="predicateAsync">The predicate.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}"/>.</returns>
    public static async Task<Result<T>> ToResultIfAsync<T>(
        this Task<T> task,
        Func<T, Task<bool>> predicateAsync) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicateAsync);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return await predicateAsync(value).ConfigureAwait(continueOnCapturedContext: false)
            ? new Result<T>(value)
            : new Result<T>(UnknownError.Value);
    }
}