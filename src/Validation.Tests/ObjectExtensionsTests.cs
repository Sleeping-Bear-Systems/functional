using SleepingBear.Functional.Common;
using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="ObjectExtensions" />
/// </summary>
internal static class ObjectExtensionsTests
{
    [Test]
    public static void ToResultIsNotNull_ErrorFunc_NotNull_ReturnOk()
    {
        var result = "ok".ToResultIsNotNull(UnknownError.Value.ToFunc());
        TestResult.IsOkEqualTo(result, expected: "ok");
    }

    [Test]
    public static void ToResultIsNotNull_ErrorFunc_Null_ReturnsError()
    {
        var result = default(string).ToResultIsNotNull(UnknownError.Value.ToFunc());
        TestResult.IsErrorEqualTo(result, UnknownError.Value);
    }

    [Test]
    public static void ToResultIsNotNull_NoNull_ReturnOk()
    {
        var result = "ok".ToResultIsNotNull(UnknownError.Value);
        TestResult.IsOkEqualTo(result, expected: "ok");
    }

    [Test]
    public static void ToResultIsNotNull_Null_ReturnsError()
    {
        var result = default(string).ToResultIsNotNull(UnknownError.Value);
        TestResult.IsErrorEqualTo(result, UnknownError.Value);
    }
}