namespace SleepingBear.Functional.Errors;

/// <summary>
///     Tagged error abstract base class.
/// </summary>
public abstract record TaggedError : Error
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="tag">Tag.</param>
    protected TaggedError(string? tag)
    {
        this.Tag = (tag ?? string.Empty).Trim();
    }

    /// <summary>
    ///     Tag. (optional)
    /// </summary>
    public string Tag { get; }
}