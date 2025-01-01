using System.Diagnostics.CodeAnalysis;
using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

// ReSharper disable RedundantNameQualifier

namespace SleepingBear.Functional.Testing;

/// <summary>
/// Extension methods for testing <see cref="Result{T}"/>.
/// </summary>
public static class TestResult
{
    /// <summary>
    /// Extension method to test that a <see cref="Result{T}"/> is OK.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/>.</param>
    /// <param name="action">The action to execute if OK.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static void IsOk<T>(Result<T> result, Action<T>? action = null) where T : notnull
    {
        _ = result.Tap(
            ok => { action?.Invoke(ok); },
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            error => { NUnit.Framework.Assert.Fail(error.ToString()); });
    }

    /// <summary>
    /// Check that a <see cref="Result{T}"/> is OK and the value is equal to the expected value.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/>.</param>
    /// <param name="expected">The expected value.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsOkEqualTo<T>(Result<T> result, T expected) where T : notnull
    {
        IsOk(result, actual => { NUnit.Framework.Assert.That(actual, NUnit.Framework.Is.EqualTo(expected)); });
    }

    /// <summary>
    /// Checks that a <see cref="Result{T}"/> is OK and the value is the same as the expected value. 
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/>.</param>
    /// <param name="expected">The expected value.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsOkSameAs<T>(Result<T> result, T expected) where T : notnull
    {
        IsOk(result, actual => { NUnit.Framework.Assert.That(actual, NUnit.Framework.Is.SameAs(expected)); });
    }

    /// <summary>
    /// Extension method to test that a <see cref="Result{T}"/> is error.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/>.</param>
    /// <param name="action">The action to execute if error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsError<T>(Result<T> result, Action<Error>? action = null)
        where T : notnull
    {
        _ = result.Tap(
            ok => { NUnit.Framework.Assert.Fail($"Is OK: {ok}"); },
            error => { action?.Invoke(error); });
    }

    /// <summary>
    /// Check that a <see cref="Result{T}"/> is error and the error is equal to the expected error.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/>.</param>
    /// <param name="expected">The expected error.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <typeparam name="TError">The type of the error.</typeparam>
    public static void IsErrorEqualTo<T, TError>(Result<T> result, TError expected)
        where T : notnull
    {
        _ = result.Tap(
            ok => { NUnit.Framework.Assert.Fail($"Is OK: {ok}"); },
            actual => { NUnit.Framework.Assert.That(actual, NUnit.Framework.Is.EqualTo(expected)); });
    }

    /// <summary>
    /// Extension method to test that a <see cref="Result{T}"/> is error.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/>.</param>
    /// <param name="action">The action to be executed if error.</param>
    /// <typeparam name="T">The type of the lifted</typeparam>
    /// <typeparam name="TError">The error type.</typeparam>
    public static void IsError<T, TError>(Result<T> result, Action<TError>? action = null)
        where T : notnull where TError : Error
    {
        _ = result.Tap(
            ok => { NUnit.Framework.Assert.Fail($"Is OK: {ok}"); },
            error => { TestError.Is(error, action); });
    }
}