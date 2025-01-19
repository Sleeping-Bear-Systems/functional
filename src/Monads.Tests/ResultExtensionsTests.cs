using System.Globalization;
using SleepingBear.Functional.Errors;
using TestResult = SleepingBear.Functional.Testing.TestResult;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
///     Tests for <see cref="ResultExtensions" />.
/// </summary>
internal static class ResultExtensionsTests
{
    [Test]
    public static void Bind_Error_ValidatesBehavior()
    {
        var error = "error".ToValueError();
        var result = error
            .ToResultError<int>()
            .Bind(ok => ok.ToString(CultureInfo.InvariantCulture).ToResultOk());
        TestResult.IsErrorEqualTo(result, error);
    }

    [Test]
    public static void Bind_Ok_ValidatesBehavior()
    {
        var result = 1234
            .ToResultOk()
            .Bind(ok => ok.ToString(CultureInfo.InvariantCulture).ToResultOk());
        TestResult.IsOkEqualTo(result, expected: "1234");
    }

    [Test]
    public static void ToResultError_Exception_ReturnExceptionError()
    {
        var ex = new InvalidOperationException();
        var result = ex.ToResultError<string>();
        TestResult.IsErrorEqualTo(result, ex.ToExceptionError());
    }

    [Test]
    public static void ToResultError_ReturnsError()
    {
        var error = "error".ToValueError();
        var result = error.ToResultError<string>();
        TestResult.IsErrorEqualTo(result, error);
    }

    [Test]
    public static void ToResultError_Value_ReturnsValueError()
    {
        var result = 1234.ToResultError<string, int>();
        TestResult.IsErrorEqualTo(result, 1234.ToValueError());
    }

    [Test]
    public static void ToResultOk_ReturnsOk()
    {
        var value = new object();
        var result = value.ToResultOk();
        TestResult.IsOkSameAs(result, value);
    }
}