namespace SleepingBear.Functional.Common.Tests;

/// <summary>
///     Tests for <see cref="StringExtensions" />.
/// </summary>
internal static class StringExtensionsTests
{
    [TestCase(arg1: null, arg2: null, ExpectedResult = "",
        TestName = "if value and nullValue are null, return empty string")]
    [TestCase(arg1: null, arg2: "nullValue", ExpectedResult = "nullValue",
        TestName = "if value is null but nullValue is not, return nullValue")]
    [TestCase(arg1: "value", arg2: "nullValue", ExpectedResult = "value",
        TestName = "if value is not null, return value")]
    public static string IfNull_ValidatesBehavior(string? value, string? nullValue)
    {
        return value.IfNull(nullValue);
    }

    [TestCase(arg1: null, arg2: null, ExpectedResult = "",
        TestName = "if value and defaultValue are null, return empty string")]
    [TestCase(arg1: null, arg2: "defaultValue", ExpectedResult = "defaultValue",
        TestName = "if value is null but defaultValue is not, return defaultValue")]
    [TestCase(arg1: "", arg2: "defaultValue", ExpectedResult = "defaultValue",
        TestName = "if value is empty but defaultValue is not, return defaultValue")]
    [TestCase(arg1: "value", arg2: "defaultValue", ExpectedResult = "value",
        TestName = "if value is not null or empty, return value")]
    public static string IsNullOrEmpty_ValidatesBehavior(string? value, string? defaultValue)
    {
        return value.IfNullOrEmpty(defaultValue);
    }

    [TestCase(arg1: null, arg2: null, ExpectedResult = "",
        TestName = "if value and defaultValue are null, return empty string")]
    [TestCase(arg1: null, arg2: "defaultValue", ExpectedResult = "defaultValue",
        TestName = "if value is null but defaultValue is not, return defaultValue")]
    [TestCase(arg1: "", arg2: "defaultValue", ExpectedResult = "defaultValue",
        TestName = "if value is empty but defaultValue is not, return defaultValue")]
    [TestCase(arg1: "  ", arg2: "defaultValue", ExpectedResult = "defaultValue",
        TestName = "if value is whitespace but defaultValue is not, return defaultValue")]
    [TestCase(arg1: "  ", arg2: "   ", ExpectedResult = "",
        TestName = "if value and defaultValue are whitespace, return defaultValue")]
    [TestCase(arg1: "value", arg2: "defaultValue", ExpectedResult = "value",
        TestName = "if value is not null or empty, return value")]
    public static string IsNullOrWhitespace_ValidatesBehavior(string? value, string? defaultValue)
    {
        return value.IfNullOrWhitespace(defaultValue);
    }
}
