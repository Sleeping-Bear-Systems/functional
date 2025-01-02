using System.Diagnostics.CodeAnalysis;

namespace SleepingBear.Functional.Monads;

/// <summary>
///     Monad containing either a value or a <see cref="Exception" />.
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressMessage(category: "Usage", checkId: "CA2225:Operator overloads have named alternates")]
public sealed record Exceptional<T> where T : notnull
{
    private readonly Exception? _exception;
    private readonly T? _value;

    /// <summary>
    ///     Default constructor.
    /// </summary>
    public Exceptional()
    {
        this._value = default;
        this._exception = new InvalidOperationException(message: "Default constructor called.");
        this.IsSuccess = false;
    }

    internal Exceptional(T value)
    {
        this._value = value;
        this._exception = null;
        this.IsSuccess = true;
    }

    internal Exceptional(Exception exception)
    {
        this._value = default;
        this._exception = exception;
        this.IsSuccess = false;
    }

    /// <summary>
    ///     Flag indicating monad contains a value.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     Flag indicating the monad contains an exception.
    /// </summary>
    public bool IsFailure => !this.IsSuccess;

    /// <summary>
    ///     Deconstructs the monad.
    /// </summary>
    /// <param name="isSuccess">The success flag.</param>
    /// <param name="value">The success value.</param>
    /// <param name="exception">The exception.</param>
    public void Deconstruct(out bool isSuccess, out T? value, out Exception? exception)
    {
        isSuccess = this.IsSuccess;
        value = this._value;
        exception = this._exception;
    }

    /// <summary>
    ///     Converts a value to a <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A <see cref="Exceptional{T}" />.</returns>
    public static implicit operator Exceptional<T>(T value)
    {
        return new Exceptional<T>(value);
    }

    /// <summary>
    ///     Converts a <see cref="Exception" /> to a <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>A <see cref="Exceptional{T}" />.</returns>
    public static implicit operator Exceptional<T>(Exception exception)
    {
        return new Exceptional<T>(exception);
    }
}

/// <summary>
///     Extension and helper methods for <see cref="Exceptional{T}" />.
/// </summary>
[SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
public static class Exceptional
{
    /// <summary>
    ///     Taps a <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="exceptional">The <see cref="Exceptional{T}" />.</param>
    /// <param name="successAction">The action to be executed if success.</param>
    /// <param name="exceptionAction">The action to be executed if failure.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The <see cref="Exceptional{T}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Exceptional<T> Tap<T>(
        this Exceptional<T> exceptional,
        Action<T> successAction,
        Action<Exception> exceptionAction) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(successAction);
        ArgumentNullException.ThrowIfNull(exceptionAction);

        var (isSuccess, value, exception) = exceptional;
        if (isSuccess)
        {
            successAction(value!);
        }
        else
        {
            exceptionAction(exception!);
        }

        return exceptional;
    }

    /// <summary>
    ///     Extension method for lifting a <see cref="Exception" /> to a <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="exception">The value being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Exceptional{T}" />.</returns>
    public static Exceptional<T> ToExceptionalFailure<T>(this Exception exception) where T : notnull
    {
        return new Exceptional<T>(exception);
    }

    /// <summary>
    ///     Extension method for lifting a value to a <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="value">The value being lifted.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>A <see cref="Exceptional{T}" />.</returns>
    public static Exceptional<T> ToExceptionalSuccess<T>(this T value) where T : notnull
    {
        return new Exceptional<T>(value);
    }

    /// <summary>
    ///     Wraps a function in a try-catch block and returns an <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="func"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <returns></returns>
    public static Exceptional<TOut> TryCatch<TIn, TOut, T1>(
        this TIn value,
        Func<TIn, TOut> func)
        where TIn : notnull where TOut : notnull where T1 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex;
        }
    }

    /// <summary>
    ///     Wraps a function in a try-catch block and returns an <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="func"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public static Exceptional<TOut> TryCatch<TIn, TOut, T1, T2>(
        this TIn value,
        Func<TIn, TOut> func)
        where TIn : notnull where TOut : notnull where T1 : Exception where T2 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex;
        }
        catch (T2 ex)
        {
            return ex;
        }
    }

    /// <summary>
    ///     Wraps a function in a try-catch block and returns an <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="func"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <returns></returns>
    public static Exceptional<TOut> TryCatch<TIn, TOut, T1, T2, T3>(
        this TIn value,
        Func<TIn, TOut> func)
        where TIn : notnull where TOut : notnull where T1 : Exception where T2 : Exception where T3 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex;
        }
        catch (T2 ex)
        {
            return ex;
        }
        catch (T3 ex)
        {
            return ex;
        }
    }

    /// <summary>
    ///     Wraps a function in a try-catch block and returns an <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="func"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <returns></returns>
    public static Exceptional<TOut> TryCatch<TIn, TOut, T1, T2, T3, T4>(
        this TIn value,
        Func<TIn, TOut> func)
        where TIn : notnull
        where TOut : notnull
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex;
        }
        catch (T2 ex)
        {
            return ex;
        }
        catch (T3 ex)
        {
            return ex;
        }
        catch (T4 ex)
        {
            return ex;
        }
    }

    /// <summary>
    ///     Wraps a function in a try-catch block and returns an <see cref="Exceptional{T}" />.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="func"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <returns></returns>
    public static Exceptional<TOut> TryCatch<TIn, TOut, T1, T2, T3, T4, T5>(
        this TIn value,
        Func<TIn, TOut> func)
        where TIn : notnull
        where TOut : notnull
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
        where T5 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex;
        }
        catch (T2 ex)
        {
            return ex;
        }
        catch (T3 ex)
        {
            return ex;
        }
        catch (T4 ex)
        {
            return ex;
        }
        catch (T5 ex)
        {
            return ex;
        }
    }
}