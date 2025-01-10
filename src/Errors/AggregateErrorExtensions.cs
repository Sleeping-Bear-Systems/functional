using System.Collections.Immutable;

namespace SleepingBear.Functional.Errors;

/// <summary>
///     Extension methods for <see cref="AggregateError" />.
/// </summary>
public static class AggregateErrorExtensions
{
    /// <summary>
    ///     Creates an <see cref="AggregateError" /> from a <see cref="ImmutableList{Error}" />.
    /// </summary>
    /// <param name="errors">The <see cref="Error" /> collection.</param>
    /// <returns>A <see cref="AggregateError" />.</returns>
    public static AggregateError ToAggregateError(this ImmutableList<Error> errors)
    {
        return new AggregateError(errors);
    }
}