using System.Diagnostics;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
///     Tests for <see cref="Exceptional" />.
/// </summary>
internal static class ExceptionalTests
{
    [Test]
    public static void Ctor_Exception_ReturnsFailure()
    {
        var exception = new UnreachableException(message: "failure");
        var exceptional = new Exceptional<int>(exception);
        TestExceptional.IsFailureSameAs(exceptional, exception);
    }

    [Test]
    public static void Ctor_Value_ReturnsSuccess()
    {
        var exceptional = new Exceptional<int>(value: 1234);
        TestExceptional.IsSuccessEqualTo(exceptional, expected: 1234);
        Assert.That(exceptional.IsFailure, Is.False);
    }

    [Test]
    public static void DefaultCtor_ReturnsFailure()
    {
        var exceptional = new Exceptional<object>();
        TestExceptional.IsFailure<object, InvalidOperationException>(
            exceptional,
            ex => { Assert.That(ex.Message, Is.EqualTo(expected: "Default constructor called.")); });
        Assert.That(exceptional.IsFailure, Is.True);
    }

    [Test]
    public static void ImplicitOperator_Exception_ReturnsFailure()
    {
        var exception = new UnreachableException(message: "failure");
        Exceptional<int> exceptional = exception;
        TestExceptional.IsFailureSameAs(exceptional, exception);
    }

    [Test]
    public static void ImplicitOperator_Value_ReturnsSuccess()
    {
        Exceptional<int> exceptional = 1234;
        TestExceptional.IsSuccessEqualTo(exceptional, expected: 1234);
    }

    [Test]
    public static void ToExceptionalFailure_REturnsFailure()
    {
        var exception = new UnreachableException(message: "failure");
        var exceptional = exception.ToExceptionalFailure<int>();
        TestExceptional.IsFailureSameAs(exceptional, exception);
    }

    [Test]
    public static void ToExceptionalSuccess_ReturnsSuccess()
    {
        var exceptional = 1234.ToExceptionalSuccess();
        TestExceptional.IsSuccessEqualTo(exceptional, expected: 1234);
    }
}