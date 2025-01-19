namespace SleepingBear.Functional.Errors.Tests;

/// <summary>
///     Tests for <see cref="InvalidFormatError" />.
/// </summary>
internal static class InvalidFormatErrorTests
{
    [Test]
    public static void Ctor_NullTag_EmptyTag()
    {
        var error = new InvalidFormatError();
        Assert.That(error.Tag, Is.Empty);
    }

    [Test]
    public static void Ctor_ValidatesBehavior()
    {
        var error = new InvalidFormatError(tag: "tag");
        Assert.That(error.Tag, Is.EqualTo(expected: "tag"));
    }
}