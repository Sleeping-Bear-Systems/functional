using NUnit.Framework;
using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Testing;

/// <summary>
///     Methods for testing <see cref="Option{T}" />.
/// </summary>
public static class TestOption
{
    /// <summary>
    ///     Tests a <see cref="Option{T}" /> is none.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}" />.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsNone<T>(Option<T> option) where T : notnull
    {
        Assert.That(option.IsNone, Is.True);
    }

    /// <summary>
    ///     Tests a <see cref="Option{T}" /> is some.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}" />.</param>
    /// <param name="action">The action to be executed.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsSome<T>(Option<T> option, Action<T>? action = null) where T : notnull
    {
        _ = option.Tap(
            some => action?.Invoke(some),
            () => Assert.Fail(message: "Expected Some, but was None"));
    }

    /// <summary>
    ///     Tests if <see cref="Option{T}" /> is some and the value is equal to the expected value.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}" />.</param>
    /// <param name="expected">The expected some value.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsSomeEqualTo<T>(Option<T> option, T expected) where T : notnull
    {
        IsSome(option, actual => { Assert.That(actual, Is.EqualTo(expected)); });
    }

    /// <summary>
    ///     Tests if <see cref="Option{T}" /> is some and the value is equal to the expected value.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}" />.</param>
    /// <param name="expected">The expected some value.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsSomeSameAs<T>(Option<T> option, T expected) where T : notnull
    {
        IsSome(option, actual => { Assert.That(actual, Is.SameAs(expected)); });
    }
}