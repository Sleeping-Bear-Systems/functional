﻿using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
/// Tests for <see cref="Result{T}"/>.
/// </summary>
internal static class ResultTests
{
    [Test]
    public static void DefaultCtor_ReturnFailure()
    {
        var result = new Result<object>();
        TestResult.IsError<object, UnknownError>(result);
    }

    [Test]
    public static void Ctor_Ok_ReturnsOk()
    {
        var value = new object();
        var result = new Result<object>(value);
        TestResult.IsOk(
            result,
            ok => { Assert.That(ok, Is.SameAs(value)); });
    }

    [Test]
    public static void Ctor_Error_ReturnsError()
    {
        var error = new ValueError<string>(Value: "error");
        var result = new Result<string>(error);
        TestResult.IsError(
            result,
            valueError => { Assert.That(valueError, Is.EqualTo(error)); });
    }

    [Test]
    public static void ToResultOk_ReturnsOk()
    {
        var value = new object();
        var result = value.ToResultOk();
        TestResult.IsOk(
            result,
            ok => { Assert.That(ok, Is.EqualTo(value)); });
    }

    [Test]
    public static void ToResultError_ReturnsError()
    {
        var error = new ValueError<string>(Value: "error");
        var result = error.ToResultError<string>();
        var (isOk, ok, resultError) = result;
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.IsError, Is.True);
            Assert.That(isOk, Is.False);
            Assert.That(ok, Is.Null);
            Assert.That(resultError, Is.EqualTo(error));
        });
    }

    [Test]
    public static void ToResultError_Value_ReturnsValueError()
    {
        var result = 1234.ToResultError<string, int>();
        var (isOk, _, resultError) = result;
        Assert.Multiple(() =>
        {
            Assert.That(isOk, Is.False);
            Assert.That(resultError, Is.EqualTo(new ValueError<int>(Value: 1234)));
        });
    }

    [Test]
    public static void ToResultError_NoArguments_ReturnsUnknownError()
    {
        var result = Result.ToResultError<int>();
        var (isOk, _, resultError) = result;
        Assert.Multiple(() =>
        {
            Assert.That(isOk, Is.False);
            Assert.That(resultError, Is.EqualTo(UnknownError.Value));
        });
    }

    [Test]
    public static void ToResultError_Exception_ReturnExceptionError()
    {
        var ex = new InvalidOperationException();
        var result = ex.ToResultError<string>();
        var (isOk, _, resultError) = result;
        Assert.Multiple(() =>
        {
            Assert.That(isOk, Is.False);
            Assert.That(resultError, Is.EqualTo(new ExceptionError(ex)));
        });
    }
}