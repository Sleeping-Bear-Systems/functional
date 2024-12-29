namespace SleepingBear.Functional.Errors;

/// <summary>
/// Unknown Error.
/// </summary>
public sealed record UnknownError : Error
{
    private UnknownError()
    {
    }

    /// <summary>
    /// Singleton instance.
    /// </summary>
    public static readonly UnknownError Value = new();
}
