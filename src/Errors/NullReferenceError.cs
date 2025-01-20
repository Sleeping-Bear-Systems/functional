namespace SleepingBear.Functional.Errors;

/// <summary>
///     Null reference error.
/// </summary>
public sealed record NullReferenceError : TaggedError
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="tag">Tag. (optional)</param>
    public NullReferenceError(string? tag = null) : base(tag)
    {
    }
}