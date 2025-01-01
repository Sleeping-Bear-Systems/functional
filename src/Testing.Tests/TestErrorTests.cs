using SleepingBear.Functional.Errors;

namespace SleepingBear.Functional.Testing.Tests;

/// <summary>
///     Tests for <see cref="TestError" />.
/// </summary>
internal static class TestErrorTests
{
    [Test]
    public static void Is_ValidatesBehavior()
    {
        var actionCalled = false;
        TestError.Is<ValueError<int>>(
            1234.ToValueError(),
            error =>
            {
                actionCalled = true;
                Assert.That(error.Value, Is.EqualTo(expected: 1234));
            });
        Assert.That(actionCalled, Is.True);
    }

    [Test]
    public static void IsEqualTo_ValidatesBehavior()
    {
        var error = 1234.ToValueError();
        TestError.IsEqualTo(error, new ValueError<int>(Value: 1234));
    }
}