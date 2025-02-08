using SleepingBear.Functional.Common;
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
    public static Option<Error> ExceptionHandler<T1>(this Exception ex) where T1 : Exception
    {
        return ex switch
        {
            T1 t1 => t1.ToExceptionError().ToOption<Error>(),
            _ => Option<Error>.None
        };
    }

    /// <summary>
    /// </summary>
    public static Option<Error> ExceptionHandler<T1, T2>(this Exception ex)
        where T1 : Exception
        where T2 : Exception
    {
        return ex switch
        {
            T1 t1 => t1.ToExceptionError().ToOption<Error>(),
            T2 t2 => t2.ToExceptionError().ToOption<Error>(),
            _ => Option<Error>.None
        };
    }

    /// <summary>
    /// </summary>
    public static Option<Error> ExceptionHandler<T1, T2, T3>(this Exception ex)
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
    {
        return ex switch
        {
            T1 t1 => t1.ToExceptionError().ToOption<Error>(),
            T2 t2 => t2.ToExceptionError().ToOption<Error>(),
            T3 t3 => t3.ToExceptionError().ToOption<Error>(),
            _ => Option<Error>.None
        };
    }

    /// <summary>
    /// </summary>
    public static Option<Error> ExceptionHandler<T1, T2, T3, T4>(this Exception ex)
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
    {
        return ex switch
        {
            T1 t1 => t1.ToExceptionError().ToOption<Error>(),
            T2 t2 => t2.ToExceptionError().ToOption<Error>(),
            T3 t3 => t3.ToExceptionError().ToOption<Error>(),
            T4 t4 => t4.ToExceptionError().ToOption<Error>(),
            _ => Option<Error>.None
        };
    }

    /// <summary>
    /// </summary>
    public static Option<Error> ExceptionHandler<T1, T2, T3, T4, T5>(this Exception ex)
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
        where T5 : Exception
    {
        return ex switch
        {
            T1 t1 => t1.ToExceptionError().ToOption<Error>(),
            T2 t2 => t2.ToExceptionError().ToOption<Error>(),
            T3 t3 => t3.ToExceptionError().ToOption<Error>(),
            T4 t4 => t4.ToExceptionError().ToOption<Error>(),
            T5 t5 => t5.ToExceptionError().ToOption<Error>(),
            _ => Option<Error>.None
        };
    }

    /// <summary>
    /// </summary>
    public static Option<Error> ExceptionHandler<T1, T2, T3, T4, T5, T6>(this Exception ex)
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
        where T5 : Exception
        where T6 : Exception
    {
        return ex switch
        {
            T1 t1 => t1.ToExceptionError().ToOption<Error>(),
            T2 t2 => t2.ToExceptionError().ToOption<Error>(),
            T3 t3 => t3.ToExceptionError().ToOption<Error>(),
            T4 t4 => t4.ToExceptionError().ToOption<Error>(),
            T5 t5 => t5.ToExceptionError().ToOption<Error>(),
            T6 t6 => t6.ToExceptionError().ToOption<Error>(),
            _ => Option<Error>.None
        };
    }

    /// <summary>
    /// </summary>
    public static Option<Error> ExceptionHandler<T1, T2, T3, T4, T5, T6, T7>(this Exception ex)
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
        where T5 : Exception
        where T6 : Exception
        where T7 : Exception
    {
        return ex switch
        {
            T1 t1 => t1.ToExceptionError().ToOption<Error>(),
            T2 t2 => t2.ToExceptionError().ToOption<Error>(),
            T3 t3 => t3.ToExceptionError().ToOption<Error>(),
            T4 t4 => t4.ToExceptionError().ToOption<Error>(),
            T5 t5 => t5.ToExceptionError().ToOption<Error>(),
            T6 t6 => t6.ToExceptionError().ToOption<Error>(),
            T7 t7 => t7.ToExceptionError().ToOption<Error>(),
            _ => Option<Error>.None
        };
    }

    /// <summary>
    /// </summary>
    public static Option<Error> ExceptionHandler<T1, T2, T3, T4, T5, T6, T7, T8>(this Exception ex)
        where T1 : Exception
        where T2 : Exception
        where T3 : Exception
        where T4 : Exception
        where T5 : Exception
        where T6 : Exception
        where T7 : Exception
        where T8 : Exception
    {
        return ex switch
        {
            T1 t1 => t1.ToExceptionError().ToOption<Error>(),
            T2 t2 => t2.ToExceptionError().ToOption<Error>(),
            T3 t3 => t3.ToExceptionError().ToOption<Error>(),
            T4 t4 => t4.ToExceptionError().ToOption<Error>(),
            T5 t5 => t5.ToExceptionError().ToOption<Error>(),
            T6 t6 => t6.ToExceptionError().ToOption<Error>(),
            T7 t7 => t7.ToExceptionError().ToOption<Error>(),
            T8 t8 => t8.ToExceptionError().ToOption<Error>(),
            _ => Option<Error>.None
        };
    }

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

    /// <summary>
    /// </summary>
    public static Result<TOut> TryCatch<TIn, TOut>(
        this TIn value,
        Func<TIn, Result<TOut>> func,
        Func<Exception, Option<Error>> handler)
        where TIn : notnull
        where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(func);
        ArgumentNullException.ThrowIfNull(handler);

        try
        {
            return func(value);
        }
        catch (Exception ex)
        {
            ex.FailFastIfCritical(message: "SleepingBear.Functional.Validation.ExceptionExtensions.TryCatch");
            if (handler(ex).TrySome(out var some))
            {
                return some.ToResultError<TOut>();
            }

            throw;
        }
    }
}