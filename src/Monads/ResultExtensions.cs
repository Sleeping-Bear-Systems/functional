using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using SleepingBear.Functional.Errors;

namespace SleepingBear.Functional.Monads;

/// <summary>
///     Extension methods for <see cref="Result{T}" />.
/// </summary>
[SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
public static class ResultExtensions
{
    /// <summary>
    ///     Binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" />.
    /// </summary>
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
    ///     Binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" />.
    /// </summary>
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
    ///     Binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" /> asynchronously.
    /// </summary>
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
    ///     Binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" /> asynchronously.
    /// </summary>
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
    ///     Binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" />.
    /// </summary>
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
    ///     Binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" />.
    /// </summary>
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
    ///     Binds a <see cref="Result{T}" /> conditionally.
    /// </summary>
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
    ///     Binds a <see cref="Result{T}" /> conditionally.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<T> BindIf<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        Func<T, Result<T>> bindTrueFunc,
        Func<T, Result<T>> bindFalseFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(bindTrueFunc);
        ArgumentNullException.ThrowIfNull(bindFalseFunc);

        var (isOk, ok, _) = result;
        return isOk
            ? predicate(ok!)
                ? bindTrueFunc(ok!)
                : bindFalseFunc(ok!)
            : result;
    }

    /// <summary>
    ///     Binds a <see cref="Result{T}" /> conditionally.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> BindIfAsync<T>(
        this Task<Result<T>> task,
        Func<T, bool> predicate,
        Func<T, Result<T>> bindFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(bindFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, _) = result;
        return isOk && predicate(ok!)
            ? bindFunc(ok!)
            : result;
    }

    /// <summary>
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> BindIfAsync<T>(
        this Task<Result<T>> task,
        Func<T, bool> predicate,
        Func<T, Task<Result<T>>> bindFuncAsync)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(bindFuncAsync);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, _) = result;
        return isOk && predicate(ok!)
            ? await bindFuncAsync(ok!).ConfigureAwait(continueOnCapturedContext: false)
            : result;
    }

    /// <summary>
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> BindIfAsync<T>(
        this Task<Result<T>> task,
        Func<T, bool> predicate,
        Func<T, Result<T>> bindTrueFunc,
        Func<T, Result<T>> bindFalseFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(bindTrueFunc);
        ArgumentNullException.ThrowIfNull(bindFalseFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, _) = result;
        return isOk
            ? predicate(ok!)
                ? bindTrueFunc(ok!)
                : bindFalseFunc(ok!)
            : result;
    }

    /// <summary>
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> BindIfAsync<T>(
        this Task<Result<T>> task,
        Func<T, bool> predicate,
        Func<T, Task<Result<T>>> bindTrueFunc,
        Func<T, Task<Result<T>>> bindFalseFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(bindTrueFunc);
        ArgumentNullException.ThrowIfNull(bindFalseFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, _) = result;
        return isOk
            ? predicate(ok!)
                ? await bindTrueFunc(ok!).ConfigureAwait(continueOnCapturedContext: false)
                : await bindFalseFunc(ok!).ConfigureAwait(continueOnCapturedContext: false)
            : result;
    }

    /// <summary>
    ///     Checks the lifted value of a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" />.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> Check<T>(this Result<T> result, Func<T, bool> predicate, Func<Error> errorFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return result.Bind(value => predicate(value)
            ? value.ToResultOk()
            : errorFunc());
    }

    /// <summary>
    ///     Checks the lifted value of a <see cref="Result{T}" /> matches the predicate.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" />.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> Check<T>(this Result<T> result, Func<T, bool> predicate, Error error)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return result.Bind(value => predicate(value)
            ? value.ToResultOk()
            : error);
    }

    /// <summary>
    ///     Checks the lifted value of a <see cref="Result{T}" /> does not match the predicate.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" />.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> CheckNot<T>(this Result<T> result, Func<T, bool> predicate, Func<Error> errorFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return result.Bind(value => predicate(value)
            ? errorFunc()
            : value.ToResultOk());
    }

    /// <summary>
    ///     Checks the lifted value of a <see cref="Result{T}" /> does not match the predicate.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" />.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> CheckNot<T>(this Result<T> result, Func<T, bool> predicate, Error error)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return result.Bind(value => predicate(value)
            ? error
            : value.ToResultOk());
    }

    /// <summary>
    ///     Maps a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" />.
    /// </summary>
    /// <param name="result">The result being mapped.</param>
    /// <param name="mapFunc">The mapping function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Result{TOut}" />.</returns>
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
    ///     Maps a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" /> asynchronously.
    /// </summary>
    /// <param name="task">The task containing the result being mapped.</param>
    /// <param name="mapFunc">The mapping function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Task{TResult}" /> containing <see cref="Result{TOut}" />.</returns>
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
    ///     Maps a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" /> asynchronously.
    /// </summary>
    /// <param name="task">The task containing the result being mapped.</param>
    /// <param name="mapFuncAsync">The mapping function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Task{TResult}" /> containing <see cref="Result{TOut}" />.</returns>
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
    ///     Maps the error of a <see cref="Result{T}" />.
    /// </summary>
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
    ///     Maps the error of a <see cref="Result{T}" /> asynchronously.
    /// </summary>
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
    ///     Maps the error of a <see cref="Result{T}" /> asynchronously.
    /// </summary>
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
    ///     Matches a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" /> being matched.</param>
    /// <param name="errorValue">The value returned if error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The lifted value if OK, the <paramref name="errorValue" /> otherwise.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static T Match<T>(this Result<T> result, T errorValue) where T : notnull
    {
        var (isOk, ok, _) = result;
        return isOk ? ok! : errorValue;
    }

    /// <summary>
    ///     Matches a <see cref="Result{T}" /> asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Result{T}" /> being matched.</param>
    /// <param name="errorValue">The value returned if error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The lifted value if OK, the <paramref name="errorValue" /> otherwise.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<T> Match<T>(this Task<Result<T>> task, T errorValue) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, _) = result;
        return isOk ? ok! : errorValue;
    }

    /// <summary>
    ///     Matches a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" /> being matched.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The lifted value if OK, the <paramref name="errorFunc" /> output otherwise.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static T Match<T>(this Result<T> result, Func<Error, T> errorFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(errorFunc);

        var (isOk, ok, error) = result;
        return isOk ? ok! : errorFunc(error!);
    }

    /// <summary>
    ///     Matches a <see cref="Result{T}" /> asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Result{T}" /> being matched.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The lifted value if OK, the <paramref name="errorFunc" /> output otherwise.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<T> Match<T>(this Task<Result<T>> task, Func<Error, T> errorFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, error) = result;
        return isOk ? ok! : errorFunc(error!);
    }

    /// <summary>
    ///     Matches a <see cref="Result{T}" /> asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Result{T}" /> being matched.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The lifted value if OK, the <paramref name="errorFunc" /> output otherwise.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<T> Match<T>(this Task<Result<T>> task, Func<Error, Task<T>> errorFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, error) = result;
        return isOk ? ok! : await errorFunc(error!).ConfigureAwait(continueOnCapturedContext: false);
    }

    /// <summary>
    ///     Matches a <see cref="Result{TIn}" /> to a value.
    /// </summary>
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
    ///     Matches a <see cref="Result{TIn}" /> to a value asynchronously.
    /// </summary>
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
    ///     Matches a <see cref="Result{TIn}" /> to a value asynchronously.
    /// </summary>
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
    ///     Matches a <see cref="Result{T}" /> or adds an error to collection.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" />.</param>
    /// <param name="errors">The <see cref="Error" /> collection.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The lifted value if OK.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static T MatchOrAddError<T>(this Result<T> result, ref ImmutableList<Error> errors) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(errors);

        var (isOk, ok, error) = result;
        if (isOk)
        {
            return ok!;
        }

        errors = errors.Add(error!);
        return default!;
    }

    /// <summary>
    ///     Matches a <see cref="Result{T}" /> or throws an exception.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" /> being matched.</param>
    /// <param name="exceptionFunc">The function generating an exception from the error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The lifted value.</returns>
    /// <exception cref="Exception">Thrown if the result is error.</exception>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static T MatchOrThrow<T>(
        this Result<T> result,
        Func<Error, Exception> exceptionFunc) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(exceptionFunc);

        var (isOk, ok, error) = result;
        return isOk
            ? ok!
            : throw exceptionFunc(error!);
    }

    /// <summary>
    ///     Matches a <see cref="Result{T}" /> or throws an exception.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" /> being matched.</param>
    /// <param name="okFunc">The match function.</param>
    /// <param name="errorFunc">The function generating an exception from the error.</param>
    /// <typeparam name="TIn">The type of the lifted value.</typeparam>
    /// <typeparam name="TOut">The matched type.</typeparam>
    /// <returns>The lifted value.</returns>
    /// <exception cref="Exception">Thrown if the result is error.</exception>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static TOut MatchOrThrow<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> okFunc,
        Func<Error, Exception> errorFunc) where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(okFunc);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var (isOk, ok, error) = result;
        return isOk
            ? okFunc(ok!)
            : throw errorFunc(error!);
    }

    /// <summary>
    ///     Matches a <see cref="Result{T}" /> or throws an exception.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containined the <see cref="Result{T}" /> being matched.</param>
    /// <param name="okFunc">The match function.</param>
    /// <param name="errorFunc">The function generating an exception from the error.</param>
    /// <typeparam name="TIn">The type of the lifted value.</typeparam>
    /// <typeparam name="TOut">The matched type.</typeparam>
    /// <returns>The lifted value.</returns>
    /// <exception cref="Exception">Thrown if the result is error.</exception>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchOrThrowAsync<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, TOut> okFunc,
        Func<Error, Exception> errorFunc) where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFunc);
        ArgumentNullException.ThrowIfNull(errorFunc);

        var (isOk, ok, error) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isOk
            ? okFunc(ok!)
            : throw errorFunc(error!);
    }

    /// <summary>
    ///     Matches a <see cref="Result{T}" /> or throws an exception.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containined the <see cref="Result{T}" /> being matched.</param>
    /// <param name="okFuncAsync">The match function.</param>
    /// <param name="errorFuncAsync">The function generating an exception from the error.</param>
    /// <typeparam name="TIn">The type of the lifted value.</typeparam>
    /// <typeparam name="TOut">The matched type.</typeparam>
    /// <returns>The lifted value.</returns>
    /// <exception cref="Exception">Thrown if the result is error.</exception>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchOrThrowAsync<TIn, TOut>(
        this Task<Result<TIn>> task,
        Func<TIn, Task<TOut>> okFuncAsync,
        Func<Error, Task<Exception>> errorFuncAsync) where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFuncAsync);
        ArgumentNullException.ThrowIfNull(errorFuncAsync);

        var (isOk, ok, error) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isOk
            ? await okFuncAsync(ok!).ConfigureAwait(continueOnCapturedContext: false)
            : throw await errorFuncAsync(error!).ConfigureAwait(continueOnCapturedContext: false);
    }

    /// <summary>
    ///     Taps a <see cref="Result{T}" />.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<T> Tap<T>(
        this Result<T> result,
        Action<T> okAction,
        Action<Error>? errorAction = null) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(okAction);

        var (isOk, ok, error) = result;
        if (isOk)
        {
            okAction(ok!);
        }
        else
        {
            errorAction?.Invoke(error!);
        }

        return result;
    }

    /// <summary>
    ///     Taps a <see cref="Result{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> TapAsync<T>(
        this Task<Result<T>> task,
        Action<T> okAction,
        Action<Error>? errorAction = null) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okAction);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, error) = result;
        if (isOk)
        {
            okAction(ok!);
        }
        else
        {
            errorAction?.Invoke(error!);
        }

        return result;
    }

    /// <summary>
    ///     Taps a <see cref="Result{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Result<T>> TapAsync<T>(
        this Task<Result<T>> task,
        Func<T, Task> okFuncAsync,
        Func<Error, Task>? errorFuncAsync = null) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(okFuncAsync);

        var result = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isOk, ok, error) = result;
        if (isOk)
        {
            await okFuncAsync(ok!).ConfigureAwait(continueOnCapturedContext: false);
        }
        else if (errorFuncAsync is not null)
        {
            await errorFuncAsync(error!).ConfigureAwait(continueOnCapturedContext: false);
        }

        return result;
    }

    /// <summary>
    ///     Lifts a <see cref="Error" /> to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="error">The error being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> ToResultError<T>(this Error error) where T : notnull
    {
        return new Result<T>(error);
    }

    /// <summary>
    ///     Lifts a value to <see cref="ValueError{T}" /> and then to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="value">The error value being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <typeparam name="TError">The tpe of the lifted error value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> ToResultError<T, TError>(this TError value) where T : notnull where TError : notnull
    {
        return new Result<T>(value.ToValueError());
    }

    /// <summary>
    ///     Converts an exception to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="ex">The exception being lifted.</param>
    /// <typeparam name="T">The type of the value being lifted.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> ToResultError<T>(this Exception ex) where T : notnull
    {
        return new ExceptionError(ex).ToResultError<T>();
    }

    /// <summary>
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="value">The value being lifted.</param>
    /// <param name="condition">The flag indicating the value is OK.</param>
    /// <param name="error">The <see cref="Error" />.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" /> containing the value.</returns>
    public static Result<T> ToResultIf<T>(
        this T value,
        bool condition,
        Error error) where T : notnull
    {
        return condition
            ? new Result<T>(value)
            : new Result<T>(error);
    }

    /// <summary>
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="value">The value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="value">The value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="value">The value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally and asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing the value.</param>
    /// <param name="flag">The flag indicating the value is OK.</param>
    /// <param name="error">The <see cref="Error" />.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Task{TResult}" /> containing the <see cref="Result{T}" />.</returns>
    public static async Task<Result<T>> ToResultIfAsync<T>(
        this Task<T> task,
        bool flag,
        Error error) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        return flag
            ? new Result<T>(await task.ConfigureAwait(continueOnCapturedContext: false))
            : new Result<T>(error);
    }

    /// <summary>
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being lifted.</param>
    /// <param name="predicateAsync">The predicate.</param>
    /// <param name="errorFuncAsync">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being lifted.</param>
    /// <param name="predicateAsync">The predicate.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being lifted.</param>
    /// <param name="predicate">The predicate.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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
    ///     Lifts a value to a <see cref="Result{T}" /> conditionally.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being lifted.</param>
    /// <param name="predicateAsync">The predicate.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
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

    /// <summary>
    ///     Lifts a value to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="ok">The value being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> ToResultOk<T>(this T ok) where T : notnull
    {
        return new Result<T>(ok);
    }

    /// <summary>
    ///     Tries to get the error of a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" />.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>True if OK, false otherwise.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static bool TryError<T>(this Result<T> result, [NotNullWhen(returnValue: true)] out Error? error)
        where T : notnull
    {
        var (isOk, _, errorValue) = result;
        error = errorValue!;
        return !isOk;
    }

    /// <summary>
    ///     Tries to get the value of a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" />.</param>
    /// <param name="ok">The lifted value.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>True if OK, false otherwise.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static bool TryOk<T>(this Result<T> result, [NotNullWhen(returnValue: true)] out T? ok) where T : notnull
    {
        var (isOk, okValue, _) = result;
        ok = okValue!;
        return isOk;
    }
}