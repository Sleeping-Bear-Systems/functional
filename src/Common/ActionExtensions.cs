// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace SleepingBear.Functional.Common;

/// <summary>
/// Extensions for <see cref="Action"/>.
/// </summary>
public static class ActionExtensions
{
    /// <summary>
    /// Converts a <see cref="Action{T1}"/> to a <see cref="Func{T1, Unit}"/>.
    /// </summary>
    /// <param name="action"></param>
    /// <typeparam name="T1"></typeparam>
    /// <returns></returns>
    public static Func<T1, Unit> ToFunc<T1>(this Action<T1> action)
    {
        return arg =>
        {
            action(arg);
            return Unit.Value;
        };
    }

    /// <summary>
    /// Converts a <see cref="Action{T1, T2}"/> to a <see cref="Func{T1, T2, Unit}"/>.
    /// </summary>
    /// <param name="action"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public static Func<T1, T2, Unit> ToFunc<T1, T2>(this Action<T1, T2> action)
    {
        return (arg1, arg2) =>
        {
            action(arg1, arg2);
            return Unit.Value;
        };
    }

    /// <summary>
    /// Converts a <see cref="Action{T1, T2,T3}"/> to a <see cref="Func{T1, T2, T3, Unit}"/>.
    /// </summary>
    /// <param name="action"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <returns></returns>
    public static Func<T1, T2, T3, Unit> ToFunc<T1, T2, T3>(this Action<T1, T2, T3> action)
    {
        return (arg1, arg2, arg3) =>
        {
            action(arg1, arg2, arg3);
            return Unit.Value;
        };
    }
}
