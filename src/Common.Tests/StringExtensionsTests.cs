namespace SleepingBear.Functional.Common.Tests;

/// <summary>
/// Tests for <see cref="StringExtensions"/>.
/// </summary>
internal static class StringExtensionsTests
{
    [Test]
    public static void IfNull_ValidDefaultValue_ReturnsValue()
    {
        var result = default(string).IfNull("value");
        Assert.That(result, Is.EqualTo("value"));
    }

    [Test]
    public static void IfNull_NullDefaultValue_ReturnsEmptyString()
    {
        var result = default(string).IfNull(null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public static void IfNull_NoDefaultValue_ReturnEmptyString()
    {
        var result = default(string).IfNull();
        Assert.That(result, Is.Empty);
    }

    [Test]
    public static void IfNull_ValidString_ReturnsValidString()
    {
        var result = "value".IfNull();
        Assert.That(result, Is.EqualTo("value"));
    }
}
