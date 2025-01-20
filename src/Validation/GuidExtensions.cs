using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Validation;

/// <summary>
///     Extension methods for <see cref="Guid" />.
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    ///     Tries to convert a value to a <see cref="Guid" />.
    /// </summary>
    /// <param name="value">The value to be converted.</param>
    /// <returns>A <see cref="Option{T}" /> containing the <see cref="Guid" />.</returns>
    public static Option<Guid> AsGuid(this object? value)
    {
        return value switch
        {
            Guid g => g,
            string g => Guid.TryParse(g, out var guid) ? guid : Option<Guid>.None,
            _ => Option<Guid>.None
        };
    }

    /// <summary>
    ///     Extension method to check if a <see cref="Guid" /> is empty.
    /// </summary>
    /// <param name="value">The <see cref="Guid" />.</param>
    /// <returns>True if empty, false otherwise.</returns>
    public static bool IsEmpty(this Guid value)
    {
        return value == Guid.Empty;
    }

    /// <summary>
    ///     Extension method to check if a <see cref="Guid" /> is not empty.
    /// </summary>
    /// <param name="value">The <see cref="Guid" /> value.</param>
    /// <returns>True if not empty, false otherwise.</returns>
    public static bool IsNotEmpty(this Guid value)
    {
        return !IsEmpty(value);
    }
}