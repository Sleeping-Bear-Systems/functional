namespace SleepingBear.Functional.Errors.Tests;

/// <summary>
///     Tests for <see cref="NullReferenceError" />.
/// </summary>
internal static class NullReferenceErrorTests
{
    [Test]
    public static void Value_IsNotNull()
    {
        Assert.That(NullReferenceError.Value, Is.Not.Null);
    }
}