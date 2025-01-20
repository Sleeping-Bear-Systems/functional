using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="ExceptionExtensions" />.
/// </summary>
internal static class ExceptionExtensionsTests
{
    [Test]
    public static void TryCatch_General_HandlerNone_Throws()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            "test value".TryCatch<string, string>(
                value =>
                {
                    Assert.That(value, Is.EqualTo(expected: "test value"));
                    throw new InvalidOperationException(message: "test");
                },
                ex =>
                {
                    return ex switch
                    {
                        ArgumentException argumentException => argumentException
                            .ToExceptionError()
                            .ToOption<Error>(),
                        _ => Option<Error>.None
                    };
                }));
        Assert.That(ex.Message, Is.EqualTo(expected: "test"));
    }

    [Test]
    public static void TryCatch_General_HandlerSome_ReturnsError()
    {
        var result = "test value".TryCatch<string, string>(
            value =>
            {
                Assert.That(value, Is.EqualTo(expected: "test value"));
                throw new InvalidOperationException(message: "test");
            },
            ex =>
            {
                return ex switch
                {
                    InvalidOperationException invalidOperationException => invalidOperationException
                        .ToExceptionError()
                        .ToOption<Error>(),
                    _ => Option<Error>.None
                };
            });
        TestResult.IsError<string, ExceptionError>(
            result,
            error =>
            {
                Assert.That(error.Exception, Is.InstanceOf<InvalidOperationException>());
                Assert.That(error.Exception.Message, Is.EqualTo(expected: "test"));
            });
    }
}