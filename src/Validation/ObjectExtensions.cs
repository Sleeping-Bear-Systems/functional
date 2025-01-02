using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

// ReSharper disable UnusedMember.Global

namespace SleepingBear.Functional.Validation;

/// <summary>
///     Extension methods for <see cref="object" />.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    ///     Lifts a nullable value to a <see cref="Result{TOk}" /> if the value is not null.
    /// </summary>
    /// <param name="value">The nullable value.</param>
    /// <param name="error">The error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> ToResultIsNotNull<T>(this T? value, Error error)
        where T : class
    {
        return value?.ToResultOk() ?? error.ToResultError<T>();
    }

    /// <summary>
    ///     Lifts a nullable value to a <see cref="Result{TOk}" /> if the value is not null.
    /// </summary>
    /// <param name="value">The nullable value.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Result{T}" />.</returns>
    public static Result<T> ToResultIsNotNull<T>(this T? value, Func<Error> errorFunc)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(errorFunc);

        return value?.ToResultOk() ?? errorFunc().ToResultError<T>();
    }
}