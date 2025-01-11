using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="Validation.DecimalExtensions" />.
/// </summary>
internal static class DecimalExtensionsTests
{
    [TestCase(arguments: null, ExpectedResult = "None", TestName = "null value")]
    [TestCase(arguments: "1234", ExpectedResult = "Some|1234", TestName = "valid string value")]
    [TestCase(arguments: 1234, ExpectedResult = "Some|1234", TestName = "valid decimal value")]
    public static string AsDecimal_ValidatesBehavior(object? value)
    {
        return value.AsDecimal().Match(some => $"Some|{some}", () => "None");
    }
}