using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Testing.Tests;

/// <summary>
///     Tests for <see cref="TestExceptional" />.
/// </summary>
internal static class TestExceptionalTests
{
    [Test]
    public static void IsFailure_Action_ValidatesBehavior()
    {
        var exceptional = new InvalidOperationException(message: "test").ToExceptionalFailure<string>();
        TestExceptional.IsFailure<string, InvalidOperationException>(
            exceptional,
            ex => Assert.That(ex.Message, Is.EqualTo(expected: "test")));
    }

    [Test]
    public static void IsFailure_NoArguments_ValidatesBehavior()
    {
        var exceptional = new InvalidOperationException().ToExceptionalFailure<string>();
        TestExceptional.IsFailure<string, InvalidOperationException>(exceptional);
    }

    [Test]
    public static void IsFailureSameAs_ValidatesBehavior()
    {
        var exception = new InvalidOperationException();
        var exceptional = exception.ToExceptionalFailure<string>();
        TestExceptional.IsFailureSameAs(exceptional, exception);
    }

    [Test]
    public static void IsSuccess_Action_ValidatesBehavior()
    {
        var exceptional = 1234.ToExceptionalSuccess();
        TestExceptional.IsSuccess(exceptional, value => Assert.That(value, Is.EqualTo(expected: 1234)));
    }

    [Test]
    public static void IsSuccessSameAs_ValidatesBehavior()
    {
        var value = new object();
        var exceptional = value.ToExceptionalSuccess();
        TestExceptional.IsSuccessSameAs(exceptional, value);
    }
}