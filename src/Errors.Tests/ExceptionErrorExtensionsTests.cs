namespace SleepingBear.Functional.Errors.Tests;

/// <summary>
///     Tests for <see cref="ExceptionErrorExtensions" />.
/// </summary>
internal static class ExceptionErrorExtensionsTests
{
    [Test]
    public static void ToExceptionError_ValidatesBehavior()
    {
        var exception = new InvalidOperationException();
        var exceptionError = exception.ToExceptionError();
        using (Assert.EnterMultipleScope())
        { Assert.That(exceptionError.Exception, Is.SameAs(exception)); }
    }
}