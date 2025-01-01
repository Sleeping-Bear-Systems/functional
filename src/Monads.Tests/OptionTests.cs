using System.Globalization;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
///     Test for <see cref="Option{T}" />.
/// </summary>
internal static class OptionTests
{
    [Test]
    public static void Bind_None_ReturnsNone()
    {
        var option = Option<int>.None.Bind(x => x.ToString(CultureInfo.InvariantCulture).ToOption());
        Assert.That(option.IsNone, Is.True);
    }

    [Test]
    public static void Bind_Some_ReturnsSome()
    {
        var option = 1234.ToOption().Bind(x => x.ToString(CultureInfo.InvariantCulture).ToOption());
        TestOption.IsSomeEqualTo(option, expected: "1234");
    }

    [Test]
    public static void Ctor_Some_ReturnsSome()
    {
        var value = new object();
        var option = new Option<object>(value);
        TestOption.IsSomeSameAs(option, value);
    }

    [Test]
    public static void DefaultCtor_ReturnsNone()
    {
        var option = new Option<object>();
        Assert.That(option.IsNone, Is.True);
    }

    [Test]
    public static void ImplicitOperator_NotNull_ReturnsSome()
    {
        Option<string> option = "test";
        TestOption.IsSomeEqualTo(option, expected: "test");
    }

    [Test]
    public static void ImplicitOperator_Null_ReturnsNone()
    {
        Option<string> option = null;
        Assert.That(option.IsNone);
    }

    [Test]
    public static void Map_None_ReturnsNone()
    {
        var option = Option<int>.None.Map(x => x.ToString(CultureInfo.InvariantCulture));
        Assert.That(option.IsNone, Is.True);
    }

    [Test]
    public static void Map_Some_ReturnsSome()
    {
        var option = 1234.ToOption().Map(x => x.ToString(CultureInfo.InvariantCulture));
        TestOption.IsSomeEqualTo(option, expected: "1234");
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
        TestOption.IsSomeSameAs(option, value);
    }

    [Test]
    public static void ToOption_Null_ReturnsNone()
    {
        var option = default(object).ToOption();
        Assert.That(option.IsNone, Is.True);
    }

    [Test]
    public static void ToOption_PredicateFalse_ReturnsNone()
    {
        var option = 1234.ToOption(_ => false);
        Assert.That(option.IsNone, Is.True);
    }

    [Test]
    public static void ToOption_PredicateTrue_ReturnsSome()
    {
        var option = 1234.ToOption(_ => true);
        TestOption.IsSomeEqualTo(option, expected: 1234);
    }

    [Test]
    public static void ToOption_PredicateWithNull_ReturnsNone()
    {
        var option = default(object).ToOption(_ => true);
        Assert.That(option.IsNone, Is.True);
    }
}