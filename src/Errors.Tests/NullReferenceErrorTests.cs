namespace SleepingBear.Functional.Errors.Tests;

/// <summary>
///     Tests for <see cref="NullReferenceError" />.
/// </summary>
internal static class NullReferenceErrorTests
{
    [Test]
    public static void Ctor_NullTag_EmptyTag()
    {
        var error = new NullReferenceError();
        Assert.That(error.Tag, Is.Empty);
    }

    [Test]
    public static void Ctor_ValidatesBehavior()
    {
        var error = new NullReferenceError(tag: "tag");
        Assert.That(error.Tag, Is.EqualTo(expected: "tag"));
    }
}