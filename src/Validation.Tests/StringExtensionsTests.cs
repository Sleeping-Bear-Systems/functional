using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="StringExtensions" />.
/// </summary>
internal static class StringExtensionsTests
{
    [Test]
    public static void ToOptionIsNotNullOrEmpty_Empty_ReturnNone()
    {
        var option = "".ToOptionIsNotNullOrEmpty();
        TestOption.IsNone(option);
    }

    [Test]
    public static void ToOptionIsNotNullOrEmpty_Null_ReturnNone()
    {
        var option = default(string).ToOptionIsNotNullOrEmpty();
        TestOption.IsNone(option);
    }

    [Test]
    public static void ToOptionIsNotNullOrEmpty_Valid_ReturnSome()
    {
        var option = "some".ToOptionIsNotNullOrEmpty();
        TestOption.IsSomeEqualTo(option, expected: "some");
    }

    [Test]
    public static void ToOptionIsNotNullOrWhiteSpace_Empty_ReturnNone()
    {
        var option = "".ToOptionIsNotNullOrWhiteSpace();
        TestOption.IsNone(option);
    }

    [Test]
    public static void ToOptionIsNotNullOrWhiteSpace_Null_ReturnNone()
    {
        var option = default(string).ToOptionIsNotNullOrWhiteSpace();
        TestOption.IsNone(option);
    }

    [Test]
    public static void ToOptionIsNotNullOrWhiteSpace_Valid_ReturnSome()
    {
        var option = "some".ToOptionIsNotNullOrWhiteSpace();
        TestOption.IsSomeEqualTo(option, expected: "some");
    }

    [Test]
    public static void ToOptionIsNotNullOrWhiteSpace_Whitespace_ReturnNone()
    {
        var option = "   ".ToOptionIsNotNullOrWhiteSpace();
        TestOption.IsNone(option);
    }

    [Test]
    public static void ToResultIsNotNullOrEmpty_Empty_ReturnError()
    {
        var result = "".ToResultIsNotNullOrEmpty(UnknownError.Value);
        TestResult.IsError<string, UnknownError>(result);
    }

    [Test]
    public static void ToResultIsNotNullOrEmpty_Null_ReturnError()
    {
        var result = default(string).ToResultIsNotNullOrEmpty(UnknownError.Value);
        TestResult.IsError<string, UnknownError>(result);
    }

    [Test]
    public static void ToResultIsNotNullOrEmpty_Valid_ReturnsOk()
    {
        var result = "ok".ToResultIsNotNullOrEmpty(UnknownError.Value);
        TestResult.IsOkEqualTo(result, expected: "ok");
    }

    [Test]
    public static void ToResultIsNotNullOrWhiteSpace_Empty_ReturnError()
    {
        var result = "".ToResultIsNotNullOrWhiteSpace(UnknownError.Value);
        TestResult.IsError<string, UnknownError>(result);
    }

    [Test]
    public static void ToResultIsNotNullOrWhiteSpace_Null_ReturnError()
    {
        var result = default(string).ToResultIsNotNullOrWhiteSpace(UnknownError.Value);
        TestResult.IsError<string, UnknownError>(result);
    }

    [Test]
    public static void ToResultIsNotNullOrWhiteSpace_Valid_ReturnsOk()
    {
        var result = "ok".ToResultIsNotNullOrWhiteSpace(UnknownError.Value);
        TestResult.IsOkEqualTo(result, expected: "ok");
    }

    [Test]
    public static void ToResultIsNotNullOrWhiteSpace_Whitespace_ReturnError()
    {
        var result = "   ".ToResultIsNotNullOrWhiteSpace(UnknownError.Value);
        TestResult.IsError<string, UnknownError>(result);
    }
}