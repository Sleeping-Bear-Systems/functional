namespace SleepingBear.Functional.Errors.Tests;

/// <summary>
///     Tests for <see cref="ExceptionError" />.
/// </summary>
internal static class ExceptionErrorTests
{
    [Test]
    public static void From_ReturnsExceptionError()
    {
        var ex = new InvalidOperationException();
        var error = ExceptionError.FromException(ex);
        Assert.That(error.Exception, Is.SameAs(ex));
    }

    [Test]
    public static void ImplicitOperator_Exception_ReturnExceptionError()
    {
        var ex = new InvalidOperationException();
        ExceptionError error = ex;
        Assert.That(error.Exception, Is.SameAs(ex));
    }
}