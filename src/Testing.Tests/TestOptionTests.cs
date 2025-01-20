using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Testing.Tests;

/// <summary>
///     Tests for <see cref="TestOption" />.
/// </summary>
internal static class TestOptionTests
{
    [Test]
    public static void IsSome_Some_ValidatesBehavior()
    {
        var option = 1234.ToOption();
        var actionCalled = false;
        TestOption.IsSome(
            option,
            value =>
            {
                actionCalled = true;
                Assert.That(value, Is.EqualTo(expected: 1234));
            });
        Assert.That(actionCalled, Is.True);
    }

    [Test]
    public static void IsSomeEqualTo_Some_ValidatesBehavior()
    {
        TestOption.IsSomeEqualTo(1234.ToOption(), expected: 1234);
    }

    [Test]
    public static void IsSomeSameAs_Some_ValidatesBehavior()
    {
        var value = new object();
        TestOption.IsSomeSameAs(value.ToOption(), value);
    }
}