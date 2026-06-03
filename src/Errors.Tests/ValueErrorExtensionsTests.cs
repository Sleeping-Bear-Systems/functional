namespace SleepingBear.Functional.Errors.Tests;

internal static class ValueErrorExtensionsTests
{
    [Test]
    public static void ToValueError_ValidatesBehavior()
    {
        var exception = new InvalidOperationException();
        var error = exception.ToValueError();
        using (Assert.EnterMultipleScope())
        { Assert.That(error.Value, Is.EqualTo(exception)); }
    }
}