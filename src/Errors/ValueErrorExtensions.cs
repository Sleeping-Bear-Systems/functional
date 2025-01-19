namespace SleepingBear.Functional.Errors;

/// <summary>
///     Extension methods for <see cref="ValueError{T}" />.
/// </summary>
public static class ValueErrorExtensions
{
    /// <summary>
    ///     Lifts a value to a <see cref="ValueError{T}" />.
    /// </summary>
    /// <param name="value">The error value.</param>
    /// <typeparam name="T">The type of the error value.</typeparam>
    /// <returns>A <see cref="ValueError{T}" />.</returns>
    public static ValueError<T> ToValueError<T>(this T value) where T : notnull
    {
        return new ValueError<T>(value);
    }
}