namespace SleepingBear.Functional.Errors;

/// <summary>
/// Extension methods for <see cref="ExceptionError"/>.
/// </summary>
public static class ExceptionErrorExtensions
{
    /// <summary>
    /// Extension method to convert an <see cref="Exception"/> to
    /// an <see cref="ExceptionError"/>.
    /// </summary>
    /// <param name="ex">The exception being lifted.</param>
    /// <returns>A <see cref="ExceptionError"/>.</returns>
    public static ExceptionError ToExceptionError(this Exception ex)
    {
        return new ExceptionError(ex);
    }
}