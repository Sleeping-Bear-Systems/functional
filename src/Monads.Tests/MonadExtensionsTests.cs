using SleepingBear.Functional.Errors;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
/// Tests for <see cref="MonadExtensions"/>.
/// </summary>
internal static class MonadExtensionsTests
{
    [Test]
    public static void ToResult_ErrorFuncIsSome_ReturnsSome()
    {
        var result = 1234.ToOption().ToResult(() => UnknownError.Value);
        var (isOk, ok, _) = result;
        Assert.Multiple(() =>
        {
            Assert.That(isOk, Is.True);
            Assert.That(ok, Is.EqualTo(expected: 1234));
        });
    }

    [Test]
    public static void ToResult_ErrorIsSome_ReturnsSome()
    {
        var result = 1234.ToOption().ToResult(UnknownError.Value);
        var (isOk, ok, _) = result;
        Assert.Multiple(() =>
        {
            Assert.That(isOk, Is.True);
            Assert.That(ok, Is.EqualTo(expected: 1234));
        });
    }

    [Test]
    public static void ToResult_ErrorFuncIsNone_ReturnsError()
    {
        var result = Option<int>.None.ToResult(() => UnknownError.Value);
        var (isOk, _, error) = result;
        Assert.Multiple(() =>
        {
            Assert.That(isOk, Is.False);
            Assert.That(error, Is.EqualTo(UnknownError.Value));
        });
    }

    [Test]
    public static void ToResult_ErrorIsNone_ReturnsError()
    {
        var result = Option<int>.None.ToResult(UnknownError.Value);
        var (isOk, _, error) = result;
        Assert.Multiple(() =>
        {
            Assert.That(isOk, Is.False);
            Assert.That(error, Is.EqualTo(UnknownError.Value));
        });
    }

    [Test]
    public static void ToOption_IsError_ReturnsNone()
    {
        var option = UnknownError.Value.ToResultError<string>().ToOption();
        Assert.That(option.IsNone, Is.True);
    }
    
    [Test]
    public static void ToOption_IsOk_ReturnsSome()
    {
        var option = "value".ToResultOk().ToOption();
        var (isSome, some) = option;
        Assert.Multiple(() =>
        {
            Assert.That(option.IsSome, Is.True);
            Assert.That(isSome, Is.True);
            Assert.That(some, Is.EqualTo(expected: "value"));
        });
    }
}