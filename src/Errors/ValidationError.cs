namespace SleepingBear.Functional.Errors;

/// <summary>
/// Validation error.
/// </summary>
public sealed record ValidationError : Error
{
    /// <summary>
    /// Message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Tag.
    /// </summary>
    public string Tag { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tag"></param>
    public ValidationError(string message, string? tag = null)
    {
        this.Message = message;
        this.Tag = tag ?? string.Empty;
    }
}
