using SleepingBear.Functional.Testing;

namespace SleepingBear.Functional.Monads.Tests;

/// <summary>
///     Test for <see cref="Option{T}" />.
/// </summary>
internal static class OptionTests
{
    [Test]
    public static void Ctor_Some_ReturnsSome()
    {
        var value = new object();
        var option = new Option<object>(value);
        TestOption.IsSomeSameAs(option, value);
    }

    [Test]
    public static void DefaultCtor_ReturnsNone()
    {
        var option = new Option<object>();
        TestOption.IsNone(option);
    }

    [Test]
    public static void ImplicitOperator_NotNull_ReturnsSome()
    {
        Option<string> option = "test";
        TestOption.IsSomeEqualTo(option, expected: "test");
    }

    [Test]
    public static void ImplicitOperator_Null_ReturnsNone()
    {
        Option<string> option = null;
        TestOption.IsNone(option);
    }

    [Test]
    public static void None_ValidatesBehavior()
    {
        TestOption.IsNone(Option<object>.None);
    }
}