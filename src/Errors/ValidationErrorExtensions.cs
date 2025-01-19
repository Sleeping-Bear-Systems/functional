namespace SleepingBear.Functional.Errors;

/// <summary>
///     Extension methods for <see cref="ValidationError" />.
/// </summary>
public static class ValidationErrorExtensions
{
    /// <summary>
    ///     Converts an error message to a <see cref="ValidationError" />.
    /// </summary>
    public static ValidationError ToValidationError(this string message, string? tag = null)
    {
        return new ValidationError(message, tag);
    }
}