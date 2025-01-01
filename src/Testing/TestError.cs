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
    /// <typeparam name="TError">The error type.</typeparam>
    public static void Is<TError>(Error error, Action<TError>? action = null)
        where TError : Error
    {
        if (error is TError concreteError)
        {
            action?.Invoke(concreteError);
        }
        else
        {
            NUnit.Framework.Assert.Fail($"Error is not of the expected type: {typeof(TError).FullName}");
        }
    }
}