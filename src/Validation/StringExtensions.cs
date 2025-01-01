using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Validation;

/// <summary>
///     Extension methods for <see cref="string" />.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Lifts a <see cref="string" /> to a <see cref="Option{T}" /> if the string is not null or empty.
    /// </summary>
    /// <param name="value">The <see cref="string" /> value.</param>
    /// <returns>A <see cref="Option{T}" /> containing the string.</returns>
    public static Option<string> ToOptionIsNotNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value)
            ? Option<string>.None
            : value;
    }

    /// <summary>
    ///     Lifts a <see cref="string" /> to a <see cref="Option{T}" /> if the string is not null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The <see cref="string" /> value.</param>
    /// <returns>A <see cref="Option{T}" /> containing the string.</returns>
    public static Option<string> ToOptionIsNotNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? Option<string>.None
            : value;
    }

    /// <summary>
    ///     Lifts a <see cref="string" /> to a <see cref="Result{TOk}" /> if the string is not null or empty.
    /// </summary>
    /// <param name="value">The <see cref="string" /> value.</param>
    /// <param name="error">The error.</param>
    /// <returns>A <see cref="Result{T}" /> containing the string.</returns>
    public static Result<string> ToResultIsNotNullOrEmpty(this string? value, Error error)
    {
        return string.IsNullOrEmpty(value)
            ? error
            : value;
    }

    /// <summary>
    ///     Lifts a <see cref="string" /> to a <see cref="Result{TOk}" /> if the string is not null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The <see cref="string" /> value.</param>
    /// <param name="error">The error.</param>
    /// <returns>A <see cref="Result{T}" /> containing the string.</returns>
    public static Result<string> ToResultIsNotNullOrWhiteSpace(this string? value, Error error)
    {
        return string.IsNullOrWhiteSpace(value)
            ? error
            : value;
    }
}