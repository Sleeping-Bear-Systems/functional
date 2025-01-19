namespace SleepingBear.Functional.Errors;

/// <summary>
///     Validation error.
/// </summary>
public sealed record ValidationError : TaggedError
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="tag">The tag. (optional)</param>
    public ValidationError(string message, string? tag = null) : base(tag)
    {
        this.Message = message;
    }

    /// <summary>
    ///     Message.
    /// </summary>
    public string Message { get; }
}