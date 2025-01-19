namespace SleepingBear.Functional.Errors;

/// <summary>
///     Invalid format error.
/// </summary>
public sealed record InvalidFormatError
{
    /// <summary>
    ///     Singleton instance.
    /// </summary>
    public static readonly InvalidFormatError Value = new();

    private InvalidFormatError()
    {
    }
}