using SleepingBear.Functional.Common;
using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Validation;

/// <summary>
///     Extension methods for <see cref="string" />.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Tokenizes a <see cref="string" />.
    /// </summary>
    public static Result<string> AsToken(this string? value, Func<Error> errorFunc)
    {
        ArgumentNullException.ThrowIfNull(errorFunc);

        return value.Tokenize().ToResultOk().CheckNot(string.IsNullOrWhiteSpace, errorFunc);
    }

    /// <summary>
    ///     Tokenizes a <see cref="string" />.
    /// </summary>
    public static Result<string> AsToken(this string? value, Error error)
    {
        return value.Tokenize().ToResultOk().CheckNot(string.IsNullOrWhiteSpace, error);
    }

    /// <summary>
    ///     Tokenizes a <see cref="string" />.
    /// </summary>
    public static Option<string> AsToken(this string? value)
    {
        return value.Tokenize().ToOption().CheckNot(string.IsNullOrWhiteSpace);
    }

    /// <summary>
    ///     Tokenizes a <see cref="string" />.
    /// </summary>
    /// <remarks>
    ///     Tokens are not null and have no preceding or trailing whitespace.
    /// </remarks>
    public static string Tokenize(this string? value)
    {
        return value.IfNull().Trim();
    }
}