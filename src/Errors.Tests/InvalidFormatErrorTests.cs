namespace SleepingBear.Functional.Errors.Tests;

/// <summary>
///     Tests for <see cref="InvalidFormatError" />.
/// </summary>
internal static class InvalidFormatErrorTests
{
    [Test]
    public static void Value_IsNotNull()
    {
        Assert.That(InvalidFormatError.Value, Is.Not.Null);
    }
}