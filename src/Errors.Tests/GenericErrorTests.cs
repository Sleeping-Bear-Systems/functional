namespace SleepingBear.Functional.Errors.Tests;

internal static class GenericErrorTests
{
    [Test]
    public static void ToGenericError_ValidatesBehavior()
    {
        var exception = new InvalidOperationException();
        var genericError = exception.ToGenericError();
        Assert.Multiple(() => { Assert.That(genericError.Value, Is.EqualTo(exception)); });
    }
}