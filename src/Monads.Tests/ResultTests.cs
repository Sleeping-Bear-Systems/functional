using SleepingBear.Functional.Errors;

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
        var (isOk, ok, error) = result;
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.IsError, Is.True);
            Assert.That(isOk, Is.False);
            Assert.That(ok, Is.Null);
            Assert.That(error, Is.InstanceOf<UnknownError>());
        });
    }

    [Test]
    public static void Ctor_Ok_ReturnsOk()
    {
        var value = new object();
        var result = new Result<object>(value);
        var (isOk, ok, error) = result;
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.True);
            Assert.That(result.IsError, Is.False);
            Assert.That(isOk, Is.True);
            Assert.That(ok, Is.EqualTo(value));
            Assert.That(error, Is.Null);
        });
    }

    [Test]
    public static void Ctor_Error_ReturnsError()
    {
        var error = new GenericError<string>("error");
        var result = new Result<string>(error);
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
    public static void ToResult_Ok_ReturnsOk()
    {
        var value = new object();
        var result = value.ToResult();
        var (isOk, ok, error) = result;
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.True);
            Assert.That(result.IsError, Is.False);
            Assert.That(isOk, Is.True);
            Assert.That(ok, Is.EqualTo(value));
            Assert.That(error, Is.Null);
        });
    }

    [Test]
    public static void ToResult_Error_ReturnsError()
    {
        var error = new GenericError<string>("error");
        var result = error.ToResult<string>();
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
}
