using SleepingBear.Functional.Errors;
using TestResult = SleepingBear.Functional.Testing.TestResult;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
///     Tests for <see cref="Result{T}" />.
/// </summary>
internal static class ResultTests
{
    [Test]
    public static void Ctor_Error_ReturnsError()
    {
        var error = "error".ToValueError();
        var result = new Result<string>(error);
        TestResult.IsErrorEqualTo(result, error);
        Assert.That(result.IsError, Is.True);
    }

    [Test]
    public static void Ctor_Ok_ReturnsOk()
    {
        var value = new object();
        var result = new Result<object>(value);
        TestResult.IsOkSameAs(result, value);
        Assert.That(result.IsError, Is.False);
    }

    [Test]
    public static void DefaultCtor_ReturnFailure()
    {
        var result = new Result<object>();
        TestResult.IsError<object, UnknownError>(result);
        Assert.That(result.IsError, Is.True);
    }

    [Test]
    public static void ImplicitOperatorError_ReturnsError()
    {
        var error = "error".ToValueError();
        Result<string> result = error;
        TestResult.IsErrorEqualTo(result, error);
    }

    [Test]
    public static void ImplicitOperatorOk_ReturnsOk()
    {
        Result<int> result = 1234;
        TestResult.IsOkEqualTo(result, expected: 1234);
    }

    [Test]
    public static void ToResultError_Exception_ReturnExceptionError()
    {
        var ex = new InvalidOperationException();
        var result = ex.ToResultError<string>();
        TestResult.IsErrorEqualTo(result, ex.ToExceptionError());
    }

    [Test]
    public static void ToResultError_NoArguments_ReturnsUnknownError()
    {
        var result = Result.ToResultError<int>();
        TestResult.IsError<int, UnknownError>(result);
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