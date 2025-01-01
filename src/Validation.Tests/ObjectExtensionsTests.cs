using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="ObjectExtensions" />
/// </summary>
internal static class ObjectExtensionsTests
{
    [Test]
    public static void ToResultIsNotNull_NoNull_ReturnOk()
    {
        var result = "ok".ToResultIsNotNull(UnknownError.Value);
        TestResult.IsOkEqualTo(result, expected: "ok");
    }

    [Test]
    public static void ToResultIsNotNull_Null_ReturnsError()
    {
        var error = 1234.ToValueError();
        var result = default(string).ToResultIsNotNull(error);
        TestResult.IsErrorEqualTo(result, error);
    }
}