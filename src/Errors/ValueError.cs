namespace SleepingBear.Functional.Errors;

/// <summary>
///     Value Error.
/// </summary>
/// <param name="Value">The value wrapped by the error.</param>
/// <typeparam name="T">The type of the wrapped value.</typeparam>
public sealed record ValueError<T>(T Value) : Error where T : notnull;

/// <summary>
///     Extension methods for <see cref="ValueError{T}" />.
/// </summary>
public static class ValueError
{
    /// <summary>
    ///     Lifts a value to a <see cref="ValueError{T}" />.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ValueError<T> ToValueError<T>(this T value) where T : notnull
    {
        return new ValueError<T>(value);
    }
}