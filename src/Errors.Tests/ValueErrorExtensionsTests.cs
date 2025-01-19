namespace SleepingBear.Functional.Errors.Tests;

internal static class ValueErrorExtensionsTests
{
    [Test]
    public static void ToValueError_ValidatesBehavior()
    {
        var exception = new InvalidOperationException();
        var error = exception.ToValueError();
        Assert.Multiple(() => { Assert.That(error.Value, Is.EqualTo(exception)); });
    }
}