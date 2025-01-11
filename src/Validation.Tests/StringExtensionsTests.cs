using SleepingBear.Functional.Common;
using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="StringExtensions" />.
/// </summary>
internal static class StringExtensionsTests
{
    [TestCase(arguments: null, ExpectedResult = "None", TestName = "null value")]
    [TestCase(arg: "test", ExpectedResult = "Some|test", TestName = "valid value")]
    public static string AsToken_Option_ValidatesBehavior(string? value)
    {
        return value.AsToken().Match(ok => $"Some|{ok}", () => "None");
    }

    [TestCase(arguments: null, ExpectedResult = "Error|UnknownError { }", TestName = "null value")]
    [TestCase(arg: "test", ExpectedResult = "OK|test", TestName = "valid value")]
    public static string AsToken_Result_Error_ValidatesBehavior(string? value)
    {
        return value.AsToken(UnknownError.Value).Match(ok => $"OK|{ok}", error => $"Error|{error}");
    }

    [TestCase(arguments: null, ExpectedResult = "Error|UnknownError { }", TestName = "null value")]
    [TestCase(arg: "test", ExpectedResult = "OK|test", TestName = "valid value")]
    public static string AsToken_Result_ErrorFunc_ValidatesBehavior(string? value)
    {
        return value.AsToken(UnknownError.Value.ToFunc()).Match(ok => $"OK|{ok}", error => $"Error|{error}");
    }

    [TestCase(arguments: null, ExpectedResult = "", TestName = "null value")]
    [TestCase(arg: "", ExpectedResult = "", TestName = "empty value")]
    [TestCase(arg: "  test   ", ExpectedResult = "test", TestName = "trim value")]
    [TestCase(arg: "test", ExpectedResult = "test", TestName = "valid value")]
    public static string Tokenize_ValidatesBehavior(string? value)
    {
        return value.Tokenize();
    }
}