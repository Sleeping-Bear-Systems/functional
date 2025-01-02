using NUnit.Framework;
using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Testing;

/// <summary>
///     Methods for testing <see cref="Exceptional{T}" />.
/// </summary>
public static class TestExceptional
{
    /// <summary>
    ///     Checks if a <see cref="Exceptional{T}" /> is failure.
    /// </summary>
    /// <param name="exceptional">The <see cref="Exceptional{T}" />.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <typeparam name="TException">The type of the exception.</typeparam>
    public static void IsFailure<T, TException>(Exceptional<T> exceptional)
        where T : notnull where TException : Exception
    {
        _ = exceptional.Tap(
            value => { Assert.Fail($"Is Success: {value}"); },
            exception => { Assert.That(exception, Is.InstanceOf<TException>()); });
    }

    /// <summary>
    ///     Checks if a <see cref="Exceptional{T}" /> is failure.
    /// </summary>
    /// <param name="exceptional">The <see cref="Exceptional{T}" />.</param>
    /// <param name="action">The action to be executed if failure.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <typeparam name="TException">The type of the exception.</typeparam>
    public static void IsFailure<T, TException>(Exceptional<T> exceptional, Action<Exception> action)
        where T : notnull where TException : Exception
    {
        _ = exceptional.Tap(
            value => { Assert.Fail($"Is Success: {value}"); },
            action);
    }

    /// <summary>
    ///     Checks if a <see cref="Exceptional{T}" /> is failure.
    /// </summary>
    /// <param name="exceptional">The <see cref="Exceptional{T}" />.</param>
    /// <param name="expected">The expected exception.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <typeparam name="TException">The type of the exception.</typeparam>
    public static void IsFailureSameAs<T, TException>(Exceptional<T> exceptional, TException expected)
        where T : notnull where TException : Exception
    {
        _ = exceptional.Tap(
            value => { Assert.Fail($"Is Success: {value}"); },
            exception => { Assert.That(exception, Is.SameAs(expected)); });
    }

    /// <summary>
    ///     Checks if a <see cref="Exceptional{T}" /> is success.
    /// </summary>
    /// <param name="exceptional">The <see cref="Exceptional{T}" />.</param>
    /// <param name="action">The action to be executed if success.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    public static void IsSuccess<T>(Exceptional<T> exceptional, Action<T> action) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(action);

        _ = exceptional.Tap(
            action,
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            exception => { Assert.Fail($"{exception.GetType().FullName} :{exception.Message}"); });
    }

    /// <summary>
    ///     Checks if a <see cref="Exceptional{T}" /> is success.
    /// </summary>
    /// <param name="exceptional">The <see cref="Exceptional{T}" />.</param>
    /// <param name="expected">The expected value.</param>
    /// <typeparam name="T"></typeparam>
    public static void IsSuccessEqualTo<T>(Exceptional<T> exceptional, T expected) where T : notnull
    {
        _ = exceptional.Tap(
            value => { Assert.That(value, Is.EqualTo(expected)); },
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            exception => { Assert.Fail($"{exception.GetType().FullName} :{exception.Message}"); });
    }

    /// <summary>
    ///     Checks if a <see cref="Exceptional{T}" /> is success.
    /// </summary>
    /// <param name="exceptional">The <see cref="Exceptional{T}" />.</param>
    /// <param name="expected">The expected value.</param>
    /// <typeparam name="T"></typeparam>
    public static void IsSuccessSameAs<T>(Exceptional<T> exceptional, T expected) where T : notnull
    {
        _ = exceptional.Tap(
            value => { Assert.That(value, Is.SameAs(expected)); },
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            exception => { Assert.Fail($"{exception.GetType().FullName} :{exception.Message}"); });
    }
}