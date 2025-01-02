using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

// ReSharper disable UnusedMember.Global

namespace SleepingBear.Functional.Validation;

/// <summary>
///     Extension methods for <see cref="Guid" />.
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    ///     Lifts a <see cref="Guid" /> to a <see cref="Option{Guid}" /> if not empty.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Option<Guid> ToOptionIsNotEmpty(this Guid value)
    {
        return value == Guid.Empty
            ? Option<Guid>.None
            : value;
    }

    /// <summary>
    ///     Lifts a <see cref="Guid" /> to a <see cref="Result{Guid}" /> if not empty.
    /// </summary>
    /// <param name="value">The <see cref="Guid" /> value.</param>
    /// <param name="error">The error.</param>
    /// <returns></returns>
    public static Result<Guid> ToResultIsNotEmpty(this Guid value, Error error)
    {
        return value == Guid.Empty
            ? error
            : value;
    }

    /// <summary>
    ///     Lifts a <see cref="Guid" /> to a <see cref="Result{Guid}" /> if not empty.
    /// </summary>
    /// <param name="value">The <see cref="Guid" /> value.</param>
    /// <param name="errorFunc">The error function.</param>
    /// <returns></returns>
    public static Result<Guid> ToResultIsNotEmpty(this Guid value, Func<Error> errorFunc)
    {
        ArgumentNullException.ThrowIfNull(errorFunc);

        return value == Guid.Empty
            ? errorFunc()
            : value;
    }
}