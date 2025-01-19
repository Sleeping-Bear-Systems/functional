namespace SleepingBear.Functional.Errors;

/// <summary>
///     Null reference error.
/// </summary>
public sealed record NullReferenceError
{
    /// <summary>
    ///     Singleton instance.
    /// </summary>
    public static readonly NullReferenceError Value = new();

    private NullReferenceError()
    {
    }
}