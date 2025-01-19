using System.Diagnostics.CodeAnalysis;
using SleepingBear.Functional.Errors;

// ReSharper disable UnusedMember.Global

namespace SleepingBear.Functional.Monads;

/// <summary>
///     Extension methods for monads.
/// </summary>
public static class MonadExtensions
{
    /// <summary>
    ///     Converts a <see cref="Result{T}" /> to a <see cref="Option{T}" />.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}" /> being converted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Option{T}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Option<T> ToOption<T>(this Result<T> result) where T : notnull
    {
        var (isOk, ok, _) = result;
        return isOk
            ? new Option<T>(ok!)
            : Option<T>.None;
    }

    /// <summary>
    ///     Converts an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}" /> being converted.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<T> ToResult<T>(this Option<T> option, Func<Error> errorFunc) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(errorFunc);

        var (isSome, some) = option;
        return isSome
            ? new Result<T>(some!)
            : new Result<T>(errorFunc());
    }

    /// <summary>
    ///     Converts an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}" /> being converted.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<T> ToResult<T>(this Option<T> option, Error error) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(error);

        var (isSome, some) = option;
        return isSome
            ? new Result<T>(some!)
            : new Result<T>(error);
    }

    /// <summary>
    ///     Converts an <see cref="Option{T}" /> to a <see cref="Result{T}" /> asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="error"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<Option<T>> task, Error error) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);

        return (await task.ConfigureAwait(continueOnCapturedContext: false)).ToResult(error);
    }
}