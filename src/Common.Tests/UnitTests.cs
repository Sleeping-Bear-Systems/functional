namespace SleepingBear.Functional.Common.Tests;

/// <summary>
///     Tests for <see cref="Unit" />.
/// </summary>
internal static class UnitTests
{
    [Test]
    public static void Unit_Value_IsNotNull()
    {
        Assert.That(Unit.Value, Is.Not.Null);
    }
}