﻿using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Testing.Tests;

/// <summary>
///     Tests for <see cref="TestResult" />.
/// </summary>
internal static class TestResultTests
{
    [Test]
    public static void IsError_ActionNotNullMatchesError_Success()
    {
        var result = UnknownError.Value.ToResultError<string>();
        var actionCalled = false;
        TestResult.IsError<string, UnknownError>(result, error =>
        {
            actionCalled = true;
            Assert.That(error, Is.InstanceOf<UnknownError>());
        });
        Assert.That(actionCalled, Is.True);
    }

    [Test]
    public static void IsError_ConcreteActionNoNull_Success()
    {
        var result = UnknownError.Value.ToResultError<string>();
        var actionCalled = false;
        TestResult.IsError(result, error =>
        {
            actionCalled = true;
            Assert.That(error, Is.InstanceOf<UnknownError>());
        });
        Assert.That(actionCalled, Is.True);
    }

    [Test]
    public static void IsError_ConcreteActionNull_Success()
    {
        var result = UnknownError.Value.ToResultError<string>();
        TestResult.IsError<string, Error>(result);
    }

    [Test]
    public static void IsError_GenericActionNull_Success()
    {
        var result = UnknownError.Value.ToResultError<string>();
        TestResult.IsError(result);
    }

    [Test]
    public static void IsErrorEqualTo_ValidatesBehavior()
    {
        var result = 1234.ToValueError().ToResultError<string>();
        TestResult.IsErrorEqualTo(result, new ValueError<int>(Value: 1234));
    }

    [Test]
    public static void IsOk_ValidatesBehavior()
    {
        var result = 1234.ToResultOk();
        var actionCalled = false;
        TestResult.IsOk(result, i =>
        {
            actionCalled = true;
            Assert.That(i, Is.EqualTo(expected: 1234));
        });
        Assert.That(actionCalled, Is.True);
    }

    [Test]
    public static void IsOkEqualTo_ValidatesBehavior()
    {
        var result = 1234.ToResultOk();
        TestResult.IsOkEqualTo(result, expected: 1234);
    }

    [Test]
    public static void IsOkSameAs_ValidatesBehavior()
    {
        var value = new object();
        var result = value.ToResultOk();
        TestResult.IsOkSameAs(result, value);
    }
}