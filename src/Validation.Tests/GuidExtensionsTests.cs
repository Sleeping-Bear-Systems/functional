using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="GuidExtensions" />.
/// </summary>
internal static class GuidExtensionsTests
{
    [Test]
    public static void ToOptionIsNotEmpty_Empty_ReturnsError()
    {
        var option = Guid.Empty.ToOptionIsNotEmpty();
        TestOption.IsNone(option);
    }

    [Test]
    public static void ToOptionIsNotEmpty_NotEmpty_ReturnsOk()
    {
        var value = new Guid(g: "37378EF9-4411-4F04-BF54-291CA21BC85B");
        var option = value.ToOptionIsNotEmpty();
        TestOption.IsSomeEqualTo(option, value);
    }

    [Test]
    public static void ToResultIsNotEmpty_Empty_ReturnsError()
    {
        var result = Guid.Empty.ToResultIsNotEmpty(UnknownError.Value);
        TestResult.IsError<Guid, UnknownError>(result);
    }

    [Test]
    public static void ToResultIsNotEmpty_NotEmpty_ReturnsOk()
    {
        var value = new Guid(g: "37378EF9-4411-4F04-BF54-291CA21BC85B");
        var result = value.ToResultIsNotEmpty(UnknownError.Value);
        TestResult.IsOkEqualTo(result, value);
    }
}