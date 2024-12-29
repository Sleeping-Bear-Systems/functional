namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
/// Test for <see cref="Option{T}"/>.
/// </summary>
internal static class OptionTests
{
    [Test]
    public static void DefaultCtor_ReturnsNone()
    {
        var option = new Option<object>();
        var (isSome, some) = option;
        Assert.Multiple(() =>
        {
            Assert.That(option.IsSome, Is.False);
            Assert.That(option.IsNone, Is.True);
            Assert.That(isSome, Is.False);
            Assert.That(some, Is.Null);
        });
    }

    [Test]
    public static void Ctor_Some_ReturnsSome()
    {
        var value = new object();
        var option = new Option<object>(value);
        var (isSome, some) = option;
        Assert.Multiple(() =>
        {
            Assert.That(option.IsSome, Is.True);
            Assert.That(option.IsNone, Is.False);
            Assert.That(isSome, Is.True);
            Assert.That(some, Is.EqualTo(value));
        });
    }

    [Test]
    public static void None_ValidatesBehavior()
    {
        Assert.That(Option<string>.None.IsNone, Is.False);
        Assert.That(Option<string>.None.IsNone, Is.True);
    }
}
