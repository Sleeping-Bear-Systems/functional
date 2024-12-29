namespace SleepingBear.Functional.Errors;

/// <summary>
/// Generic Error.
/// </summary>
/// <param name="Value">The value wrapped by the error.</param>
/// <typeparam name="T">The type of the wrapped value.</typeparam>
public sealed record GenericError<T>(T Value) : Error where T : notnull;

/// <summary>
/// Extension methods for <see cref="GenericError{T}"/>.
/// </summary>
public static class GenericError
{
    /// <summary>
    /// Lifts a value to a <see cref="GenericError{T}"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static GenericError<T> ToGenericError<T>(this T value) where T : notnull
    {
        return new GenericError<T>(value);
    }
}