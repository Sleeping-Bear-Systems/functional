using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace SleepingBear.Functional.Validation;

/// <summary>
///     Extension methods for <see cref="Exception" />.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut, T1>(this TIn value, Func<TIn, Result<TOut>> func)
        where TIn : notnull
        where TOut : notnull
        where T1 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
    }

    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut, T1, T2>(this TIn value, Func<TIn, Result<TOut>> func)
        where TIn : notnull
        where TOut : notnull
        where T1 : Exception
        where T2 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T2 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
    }

    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut, T1, T2, T3>(this TIn value, Func<TIn, Result<TOut>> func)
        where TIn : notnull
        where TOut : notnull
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T2 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T3 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
    }

    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut, T1, T2, T3, T4>(this TIn value, Func<TIn, Result<TOut>> func)
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
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T2 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T3 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T4 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
    }

    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut, T1, T2, T3, T4, T5>(this TIn value, Func<TIn, Result<TOut>> func)
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
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T2 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T3 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T4 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T5 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
    }

    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut, T1, T2, T3, T4, T5, T6>(this TIn value, Func<TIn, Result<TOut>> func)
        where TIn : notnull
        where TOut : notnull
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
        where T5 : Exception
        where T6 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T2 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T3 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T4 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T5 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T6 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
    }

    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut, T1, T2, T3, T4, T5, T6, T7>(this TIn value,
        Func<TIn, Result<TOut>> func)
        where TIn : notnull
        where TOut : notnull
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
        where T5 : Exception
        where T6 : Exception
        where T7 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T2 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T3 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T4 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T5 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T6 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T7 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
    }

    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut, T1, T2, T3, T4, T5, T6, T7, T8>(this TIn value,
        Func<TIn, Result<TOut>> func)
        where TIn : notnull
        where TOut : notnull
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
        where T5 : Exception
        where T6 : Exception
        where T7 : Exception
        where T8 : Exception
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return func(value);
        }
        catch (T1 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T2 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T3 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T4 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T5 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T6 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T7 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
        catch (T8 ex)
        {
            return ex.ToExceptionError().ToResultError<TOut>();
        }
    }
}