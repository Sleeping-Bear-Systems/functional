using System.Globalization;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
///     Test for <see cref="OptionExtensions" />.
/// </summary>
internal static class OptionExtensionsTests
{
    [Test]
    public static void Bind_None_CallsNoneFunction()
    {
        var option = Option<string>
            .None
            .Bind(
                some => some.ToString(CultureInfo.InvariantCulture),
                () => "none".ToOption());
        TestOption.IsSomeEqualTo(option, expected: "none");
    }

    [Test]
    public static void Bind_None_ReturnsNone()
    {
        var option = Option<int>.None.Bind(x => x.ToString(CultureInfo.InvariantCulture).ToOption());
        TestOption.IsNone(option);
    }

    [Test]
    public static void Bind_Some_CallsSomeFunction()
    {
        var option = 1234
            .ToOption()
            .Bind(
                some => some.ToString(CultureInfo.InvariantCulture),
                () => Option<string>.None);
        TestOption.IsSomeEqualTo(option, expected: "1234");
    }

    [Test]
    public static void Bind_Some_ReturnsSome()
    {
        var option = 1234.ToOption().Bind(x => x.ToString(CultureInfo.InvariantCulture).ToOption());
        TestOption.IsSomeEqualTo(option, expected: "1234");
    }

    [Test]
    public static void Map_None_ReturnsNone()
    {
        var option = Option<int>.None.Map(x => x.ToString(CultureInfo.InvariantCulture));
        TestOption.IsNone(option);
    }

    [Test]
    public static void Map_Some_ReturnsSome()
    {
        var option = 1234.ToOption().Map(x => x.ToString(CultureInfo.InvariantCulture));
        TestOption.IsSomeEqualTo(option, expected: "1234");
    }

    [Test]
    public static void MapNone_None_ReturnsOption()
    {
        var option = Option<int>.None.MapNone(() => 1234.ToOption());
        TestOption.IsSomeEqualTo(option, expected: 1234);
    }

    [Test]
    public static void Match_NoneFuncWithNone_ReturnsSome()
    {
        var match = Option<int>
            .None
            .Match(
                some => some.ToString(CultureInfo.InvariantCulture),
                () => "none");
        Assert.That(match, Is.EqualTo(expected: "none"));
    }

    [Test]
    public static void Match_NoneFuncWithSome_ReturnsSome()
    {
        var match = 1234
            .ToOption()
            .Match(
                some => some.ToString(CultureInfo.InvariantCulture),
                () => "none");
        Assert.That(match, Is.EqualTo(expected: "1234"));
    }

    [Test]
    public static void Match_NoneValueWithNone_ReturnsNoneValue()
    {
        var match = Option<int>.None.Match(noneValue: -1);
        Assert.That(match, Is.EqualTo(expected: -1));
    }

    [Test]
    public static void Match_NoneValueWithSome_ReturnsSome()
    {
        var match = 1234.ToOption().Match(noneValue: -1);
        Assert.That(match, Is.EqualTo(expected: 1234));
    }

    [Test]
    public static void Match_SomeFuncNoneValueWithNone_ReturnsNoneValue()
    {
        var match = Option<int>
            .None
            .Match(some => some.ToString(CultureInfo.InvariantCulture), none: "none");
        Assert.That(match, Is.EqualTo(expected: "none"));
    }

    [Test]
    public static void Match_SomeFuncNoneValueWithSome_ReturnsSomeFunc()
    {
        var match = 1234
            .ToOption()
            .Match(some => some.ToString(CultureInfo.InvariantCulture), none: "none");
        Assert.That(match, Is.EqualTo(expected: "1234"));
    }

    [Test]
    public static void Tap_None_NoneActionCalled()
    {
        var actionCalled = false;
        _ = Option<int>.None
            .Tap(
                _ => { Assert.Fail(); },
                () => { actionCalled = true; });
        Assert.That(actionCalled, Is.True);
    }

    [Test]
    public static void Tap_Some_CallSomeAction()
    {
        var actionCalled = false;
        _ = 1234
            .ToOption()
            .Tap(
                some =>
                {
                    actionCalled = true;
                    Assert.That(some, Is.EqualTo(expected: 1234));
                },
                Assert.Fail);
        Assert.That(actionCalled, Is.True);
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
        TestOption.IsNone(option);
    }

    [Test]
    public static void ToOption_PredicateFalse_ReturnsNone()
    {
        var option = 1234.ToOption(_ => false);
        TestOption.IsNone(option);
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
        TestOption.IsNone(option);
    }

    [Test]
    public static void TrySome_None_ValidatesBehavior()
    {
        var option = Option<int>.None;
        var result = option.TrySome(out var some);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(some, Is.EqualTo(expected: 0));
        });
    }

    [Test]
    public static void TrySome_Some_ValidatesBehavior()
    {
        var option = 1234.ToOption();
        var result = option.TrySome(out var some);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(some, Is.EqualTo(expected: 1234));
        });
    }
}