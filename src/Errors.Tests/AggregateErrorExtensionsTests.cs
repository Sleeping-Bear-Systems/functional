using System.Collections.Immutable;

namespace SleepingBear.Functional.Errors.Tests;

/// <summary>
///     Tests for <see cref="AggregateErrorExtensions" />.
/// </summary>
internal static class AggregateErrorExtensionsTests
{
    [Test]
    public static void ToAggregateError_ValidatesBehavior()
    {
        var errors = ImmutableList<Error>.Empty
            .Add(UnknownError.Value);
        var error = errors.ToAggregateError();
        Assert.That(error.Errors, Is.EqualTo(errors));
    }
}