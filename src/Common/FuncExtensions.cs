namespace SleepingBear.Functional.Common;

/// <summary>
///     Extension methods for <see cref="Func{TResult}" />.
/// </summary>
public static class FuncExtensions
{
    /// <summary>
    ///     Converts a value to a function.
    /// </summary>
    /// <param name="value">The return value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>A <see cref="Func{TResult}" /> that returns the value.</returns>
    public static Func<T> ToFunc<T>(this T value)
    {
        return () => value;
    }
}