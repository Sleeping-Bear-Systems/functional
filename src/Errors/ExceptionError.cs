namespace SleepingBear.Functional.Errors;

/// <summary>
///     Exception error.
/// </summary>
public sealed record ExceptionError(Exception Exception) : Error
{
    /// <summary>
    ///     Converts an <see cref="Exception" /> to an <see cref="ExceptionError" />.
    /// </summary>
    /// <param name="ex">The exception being lifted.</param>
    /// <returns>A <see cref="ExceptionError" />.</returns>
    /// <remarks>Required to satisfy CA2225.</remarks>
    public static ExceptionError FromException(Exception ex)
    {
        return new ExceptionError(ex);
    }

    /// <summary>
    ///     Implicitly converts an <see cref="Exception" /> to an <see cref="ExceptionError" />.
    /// </summary>
    /// <param name="ex">The exception being lifted.</param>
    /// <returns>A <see cref="ExceptionError" />.</returns>
    public static implicit operator ExceptionError(Exception ex)
    {
        return new ExceptionError(ex);
    }
}