using System.Collections.Immutable;

namespace SleepingBear.Functional.Errors;

/// <summary>
///     Aggregate error.
/// </summary>
public sealed record AggregateError : Error
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="errors">The <see cref="Error" /> collection.</param>
    public AggregateError(ImmutableList<Error> errors)
    {
        this.Errors = errors;
    }

    /// <summary>
    ///     The <see cref="Error" /> collection.
    /// </summary>
    public ImmutableList<Error> Errors { get; }
}