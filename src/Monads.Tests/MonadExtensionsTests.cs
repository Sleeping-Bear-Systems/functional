using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
///     Tests for <see cref="MonadExtensions" />.
/// </summary>
internal static class MonadExtensionsTests
{
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
        TestOption.IsSomeEqualTo(option, expected: "value");
    }

    [Test]
    public static void ToResult_ErrorFuncIsNone_ReturnsError()
    {
        var result = Option<int>.None.ToResult(() => UnknownError.Value);
        TestResult.IsError<int, UnknownError>(result);
    }

    [Test]
    public static void ToResult_ErrorFuncIsSome_ReturnsSome()
    {
        var result = 1234.ToOption().ToResult(() => UnknownError.Value);
        TestResult.IsOkEqualTo(result, expected: 1234);
    }

    [Test]
    public static void ToResult_ErrorIsNone_ReturnsError()
    {
        var result = Option<int>.None.ToResult(UnknownError.Value);
        TestResult.IsError<int, UnknownError>(result);
    }

    [Test]
    public static void ToResult_ErrorIsSome_ReturnsSome()
    {
        var result = 1234.ToOption().ToResult(UnknownError.Value);
        TestResult.IsErrorEqualTo(result, expected: 1234);
    }
}