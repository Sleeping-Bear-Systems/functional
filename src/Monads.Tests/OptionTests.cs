using System.Globalization;

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
        Assert.That(Option<string>.None.IsNone, Is.True);
    }

    [Test]
    public static void ToOption_NotNull_ReturnsSome()
    {
        var value = new object();
        var option = value.ToOption();
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
    public static void ToOption_Null_ReturnsNone()
    {
        var option = default(object).ToOption();
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
    public static void ToOption_PredicateWithNull_ReturnsNone()
    {
        var option = default(object).ToOption(_ => true);
        Assert.That(option.IsNone, Is.True);
    }

    [Test]
    public static void ToOption_PredicateTrue_ReturnsSome()
    {
        var option = 1234.ToOption(_ => true);
        var (isSome, some) = option;
        Assert.Multiple(() =>
        {
            Assert.That(option.IsSome, Is.True);
            Assert.That(isSome, Is.True);
            Assert.That(some, Is.EqualTo(expected: 1234));
        });
    }

    [Test]
    public static void ToOption_PredicateFalse_ReturnsSome()
    {
        var option = 1234.ToOption(_ => false);
        var (isSome, some) = option;
        Assert.Multiple(() =>
        {
            Assert.That(option.IsSome, Is.False);
            Assert.That(isSome, Is.False);
            Assert.That(some, Is.EqualTo(expected: 0));
        });
    }

    [Test]
    public static void Map_Some_ReturnsSome()
    {
        var option = 1234.ToOption().Map(x => x.ToString(CultureInfo.InvariantCulture));
        var (isSome, some) = option;
        Assert.Multiple(() =>
        {
            Assert.That(option.IsSome, Is.True);
            Assert.That(isSome, Is.True);
            Assert.That(some, Is.EqualTo(expected: "1234"));
        });
    }

    [Test]
    public static void Map_None_ReturnsNone()
    {
        var option = Option<int>.None.Map(x => x.ToString(CultureInfo.InvariantCulture));
        Assert.That(option.IsNone, Is.True);
    }

    [Test]
    public static void Bind_Some_ReturnsSome()
    {
        var option = 1234.ToOption().Bind(x => x.ToString(CultureInfo.InvariantCulture).ToOption());
        var (isSome, some) = option;
        Assert.Multiple(() =>
        {
            Assert.That(option.IsSome, Is.True);
            Assert.That(isSome, Is.True);
            Assert.That(some, Is.EqualTo(expected: "1234"));
        });
    }

    [Test]
    public static void Bind_None_ReturnsNone()
    {
        var option = Option<int>.None.Bind(x => x.ToString(CultureInfo.InvariantCulture).ToOption());
        Assert.That(option.IsNone, Is.True);
    }
}
