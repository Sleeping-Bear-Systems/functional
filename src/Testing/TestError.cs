using NUnit.Framework;
using SleepingBear.Functional.Errors;

namespace SleepingBear.Functional.Testing;

/// <summary>
/// Methods for testing <see cref="Error"/>.
/// </summary>
public static class TestError
{
    /// <summary>
    /// Tests a <see cref="Error"/> is of the expected type.
    /// </summary>
    /// <param name="error">The <see cref="Error"/>.</param>
    /// <param name="action">The action to be executed. (optional)</param>
    /// <typeparam name="T">The error type.</typeparam>
    public static void Is<T>(Error error, Action<T>? action = null)
        where T : Error
    {
        if (error is T concreteError)
        {
            action?.Invoke(concreteError);
        }
        else
        {
            Assert.Fail($"Error is not of the expected type: {typeof(T).FullName}");
        }
    }

    /// <summary>
    /// Checks if a <see cref="Error"/> is equal to the expected value.
    /// </summary>
    /// <param name="error">The <see cref="Error"/>.</param>
    /// <param name="expected">The expected error.</param>
    /// <typeparam name="T">The type of the error.</typeparam>
    public static void IsEqualTo<T>(Error error, T expected)
        where T : Error
    {
        Is<T>(error, actual => { Assert.That(actual, NUnit.Framework.Is.EqualTo(expected)); });
    }
}