using SleepingBear.Functional.Common;
using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;
using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="GuidExtensions" />.
/// </summary>
internal static class GuidExtensionsTests
{
    [Test]
    public static void CheckNotEmpty_Empty_ReturnsError()
    {
        var option = Guid.Empty.ToResultOk().CheckIsNotEmpty(UnknownError.Value);
        TestResult.IsErrorEqualTo(option, UnknownError.Value);
    }

    [Test]
    public static void CheckNotEmpty_Empty_ReturnsNone()
    {
        var option = Guid.Empty.ToOption().CheckIsNotEmpty();
        TestOption.IsNone(option);
    }

    [Test]
    public static void CheckNotEmpty_ErrorFunc_Empty_ReturnsError()
    {
        var option = Guid.Empty.ToResultOk().CheckIsNotEmpty(UnknownError.Value.ToFunc());
        TestResult.IsErrorEqualTo(option, UnknownError.Value);
    }

    [Test]
    public static void CheckNotEmpty_ErrorFunc_NotEmpty_ReturnsError()
    {
        var guid = new Guid(g: "166D26E6-64E4-48AE-AEDF-6C992F559859");
        var result = guid.ToResultOk().CheckIsNotEmpty(UnknownError.Value.ToFunc());
        TestResult.IsOkEqualTo(result, guid);
    }

    [Test]
    public static void CheckNotEmpty_NotEmpty_ReturnsError()
    {
        var guid = new Guid(g: "166D26E6-64E4-48AE-AEDF-6C992F559859");
        var result = guid.ToResultOk().CheckIsNotEmpty(UnknownError.Value);
        TestResult.IsOkEqualTo(result, guid);
    }

    [Test]
    public static void CheckNotEmpty_NotEmpty_ReturnsSome()
    {
        var option = new Guid(g: "3F47534B-96A6-48B4-A6A7-83D973CC4EF6").ToOption().CheckIsNotEmpty();
        TestOption.IsSome(option);
    }
}