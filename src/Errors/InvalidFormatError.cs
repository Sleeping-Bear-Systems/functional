namespace SleepingBear.Functional.Errors;

/// <summary>
///     Invalid format error.
/// </summary>
public sealed record InvalidFormatError : TaggedError
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="tag">Tag. (optional)</param>
    public InvalidFormatError(string? tag = null) : base(tag)
    {
    }
}