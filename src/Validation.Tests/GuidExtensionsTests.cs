using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Validation.Tests;

/// <summary>
///     Tests for <see cref="GuidExtensions" />.
/// </summary>
internal static class GuidExtensionsTests
{
    [Test]
    public static void AsGuid_Guid_ReturnsSome()
    {
        var option = Guid.Empty.AsGuid();
        TestOption.IsSomeEqualTo(option, Guid.Empty);
    }

    [Test]
    public static void AsGuid_InvalidString_ReturnsSome()
    {
        var option = "not a guid".AsGuid();
        TestOption.IsNone(option);
    }

    [Test]
    public static void AsGuid_ValidString_ReturnsSome()
    {
        var option = Guid.Empty.ToString().AsGuid();
        TestOption.IsSomeEqualTo(option, Guid.Empty);
    }

    [Test]
    public static void IsEmpty_Empty_ReturnsTrue()
    {
        Assert.That(Guid.Empty.IsEmpty(), Is.True);
    }

    [Test]
    public static void IsEmpty_NotEmpty_ReturnsFalse()
    {
        Assert.That(new Guid(g: "F6AB757B-1AC1-4E99-BC08-4BFBF5897B9B").IsEmpty(), Is.False);
    }

    [Test]
    public static void IsNotEmpty_Empty_ReturnsFalse()
    {
        Assert.That(Guid.Empty.IsNotEmpty(), Is.False);
    }

    [Test]
    public static void IsNotEmpty_NotEmpty_ReturnsTrue()
    {
        Assert.That(new Guid(g: "F6AB757B-1AC1-4E99-BC08-4BFBF5897B9B").IsNotEmpty(), Is.True);
    }
}