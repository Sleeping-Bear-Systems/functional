namespace SleepingBear.Functional.Common.Tests;

/// <summary>
///     Tests for <see cref="ExceptionExtensions" />.
/// </summary>
internal static class ExceptionExtensionsTests
{
    [Test]
    public static void FailFastIfCritical_NonCriticalException_ReturnsException()
    {
        var exception = new InvalidOperationException();
        var result = exception.FailFastIfCritical(null);
        Assert.That(result, Is.EqualTo(exception));
    }
}
