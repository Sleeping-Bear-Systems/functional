namespace SleepingBear.Functional.Common;

/// <summary>
///     Extension methods for <see cref="Func{TResult}" />.
/// </summary>
public static class FuncExtensions
{
    /// <summary>
    ///     Identity function.
    /// </summary>
    public static T Identity<T>(this T value)
    {
        return value;
    }

    /// <summary>
    ///     Converts a value to a function.
    /// </summary>
    /// <param name="value">The return value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>A <see cref="Func{TResult}" /> that returns the value.</returns>
    public static Func<T> ToFunc<T>(this T value)
    {
        return () => value;
    }

    /// <summary>
    ///     Converts an <see cref="Action" /> to a <see cref="Func{TResult}" />.
    /// </summary>
    public static Func<Unit> ToFunc(this Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        return () =>
        {
            action();
            return Unit.Value;
        };
    }

    /// <summary>
    ///     Converts an <see cref="Action{T}" /> to a <see cref="Func{TResult}" />.
    /// </summary>
    public static Func<T1, Unit> ToFunc<T1>(this Action<T1> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        return t1 =>
        {
            action(t1);
            return Unit.Value;
        };
    }

    /// <summary>
    ///     Converts an <see cref="Action{T}" /> to a <see cref="Func{TResult}" />.
    /// </summary>
    public static Func<T1, T2, Unit> ToFunc<T1, T2>(this Action<T1, T2> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        return (t1, t2) =>
        {
            action(t1, t2);
            return Unit.Value;
        };
    }

    /// <summary>
    ///     Converts an <see cref="Action{T}" /> to a <see cref="Func{TResult}" />.
    /// </summary>
    public static Func<T1, T2, T3, Unit> ToFunc<T1, T2, T3>(this Action<T1, T2, T3> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        return (t1, t2, t3) =>
        {
            action(t1, t2, t3);
            return Unit.Value;
        };
    }

    /// <summary>
    ///     Converts an <see cref="Action{T}" /> to a <see cref="Func{TResult}" />.
    /// </summary>
    public static Func<T1, T2, T3, T4, Unit> ToFunc<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        return (t1, t2, t3, t4) =>
        {
            action(t1, t2, t3, t4);
            return Unit.Value;
        };
    }

    /// <summary>
    ///     Converts an <see cref="Action{T}" /> to a <see cref="Func{TResult}" />.
    /// </summary>
    public static Func<T1, T2, T3, T4, T5, Unit> ToFunc<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        return (t1, t2, t3, t4, t5) =>
        {
            action(t1, t2, t3, t4, t5);
            return Unit.Value;
        };
    }

    /// <summary>
    ///     Converts an <see cref="Action{T}" /> to a <see cref="Func{TResult}" />.
    /// </summary>
    public static Func<T1, T2, T3, T4, T5, T6, Unit> ToFunc<T1, T2, T3, T4, T5, T6>(
        this Action<T1, T2, T3, T4, T5, T6> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        return (t1, t2, t3, t4, t5, t6) =>
        {
            action(t1, t2, t3, t4, t5, t6);
            return Unit.Value;
        };
    }

    /// <summary>
    ///     Converts a function to a unary function.
    /// </summary>
    public static Func<TIn1, TOut> ToUnaryFunc<TIn1, TIn2, TOut>(this Func<TIn1, TIn2, TOut> func, TIn2 arg2)
    {
        ArgumentNullException.ThrowIfNull(func);

        return arg1 => func(arg1, arg2);
    }

    /// <summary>
    ///     Converts a function to a unary function.
    /// </summary>
    public static Func<TIn1, TOut> ToUnaryFunc<TIn1, TIn2, TIn3, TOut>(
        this Func<TIn1, TIn2, TIn3, TOut> func,
        TIn2 arg2,
        TIn3 arg3)
    {
        ArgumentNullException.ThrowIfNull(func);

        return arg1 => func(arg1, arg2, arg3);
    }

    /// <summary>
    ///     Converts a function to a unary function.
    /// </summary>
    public static Func<TIn1, TOut> ToUnaryFunc<TIn1, TIn2, TIn3, TIn4, TOut>(
        this Func<TIn1, TIn2, TIn3, TIn4, TOut> func,
        TIn2 arg2,
        TIn3 arg3,
        TIn4 arg4)
    {
        ArgumentNullException.ThrowIfNull(func);

        return arg1 => func(arg1, arg2, arg3, arg4);
    }

    /// <summary>
    ///     Converts a function to a unary function.
    /// </summary>
    public static Func<TIn1, TOut> ToUnaryFunc<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(
        this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> func,
        TIn2 arg2,
        TIn3 arg3,
        TIn4 arg4,
        TIn5 arg5)
    {
        ArgumentNullException.ThrowIfNull(func);

        return arg1 => func(arg1, arg2, arg3, arg4, arg5);
    }

    /// <summary>
    ///     Converts a function to a unary function.
    /// </summary>
    public static Func<TIn1, TOut> ToUnaryFunc<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(
        this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> func,
        TIn2 arg2,
        TIn3 arg3,
        TIn4 arg4,
        TIn5 arg5,
        TIn6 arg6)
    {
        ArgumentNullException.ThrowIfNull(func);

        return arg1 => func(arg1, arg2, arg3, arg4, arg5, arg6);
    }
}