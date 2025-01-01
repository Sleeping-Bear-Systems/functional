using NUnit.Framework;
using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Testing;

/// <summary>
/// Methods for testing <see cref="Option{T}"/>.
/// </summary>
public static class TestOption
{
    /// <summary>
    /// Tests a <see cref="Option{T}"/> is some.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/>.</param>
    /// <param name="action">The action to be executed.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsSome<T>(Option<T> option, Action<T>? action = null) where T : notnull
    {
        _ = option.Tap(
            some => action?.Invoke(some),
            () => Assert.Fail(message: "Expected Some, but was None"));
    }
}