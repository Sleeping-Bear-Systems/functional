using System.Diagnostics.CodeAnalysis;
using SleepingBear.Functional.Errors;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace SleepingBear.Functional.Monads;

/// <summary>
/// Extension methods for monads.
/// </summary>
public static class MonadExtensions
{
    /// <summary>
    /// Converts an <see cref="Option{T}"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="option"></param>
    /// <param name="errorFunc"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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
    /// Converts an <see cref="Option{T}"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="option"></param>
    /// <param name="error"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Result<T> ToResult<T>(this Option<T> option, Error error) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(error);

        var (isSome, some) = option;
        return isSome
            ? new Result<T>(some!)
            : new Result<T>(error);
    }
}
