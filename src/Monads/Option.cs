using System.Diagnostics.CodeAnalysis;

namespace SleepingBear.Functional.Monads;

/// <summary>
/// Monad representing an optional value.
/// </summary>
/// <typeparam name="T">The type of the lifted value.</typeparam>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
public readonly record struct Option<T> where T : notnull
{
    private readonly T? _some;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Option()
    {
        this._some = default;
        this.IsSome = false;
    }

    internal Option(T some)
    {
        this._some = some;
        this.IsSome = true;
    }

    /// <summary>
    /// Flag indicating the monad contains a value.
    /// </summary>
    public bool IsSome { get; }

    /// <summary>
    /// Flag indicating the monad does not contain a value.
    /// </summary>
    public bool IsNone => !this.IsSome;

    /// <summary>
    /// None instance.
    /// </summary>
    public static readonly Option<T> None = new();

    /// <summary>
    /// Deconstructs the monad.
    /// </summary>
    /// <param name="isSome">The flag indicating the monad contains a value.</param>
    /// <param name="some">The lifted value.</param>
    public void Deconstruct(out bool isSome, out T? some)
    {
        isSome = this.IsSome;
        some = this._some;
    }
}

/// <summary>
/// Helper methods for <see cref="Option{T}"/>.
/// </summary>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
public static class Option
{
    /// <summary>
    /// Lifts a value to a <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="some">The value being lifted.</param>
    /// <typeparam name="T">The type of the value being lifted.</typeparam>
    /// <returns>A <see cref="Option{T}"/>.</returns>
    public static Option<T> ToOption<T>(this T? some) where T : notnull =>
        some is null
            ? Option<T>.None
            : new Option<T>(some);

    /// <summary>
    /// Conditionally lifts a value to a <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="some"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Option<T> ToOption<T>(this T? some, Func<T, bool> predicate) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return some is not null && predicate(some)
            ? new Option<T>(some)
            : Option<T>.None;
    }

    /// <summary>
    /// Maps a <see cref="Option{TIn}"/> to <see cref="Option{TOut}"/>.
    /// </summary>
    /// <param name="option">The <see cref="Option{TIn}"/> being mapped.</param>
    /// <param name="mapFunc">The map function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Option{TOut}"/>.</returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static Option<TOut> Map<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> mapFunc) where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(mapFunc);

        var (isSome, some) = option;
        return isSome
            ? new Option<TOut>(mapFunc(some!))
            : Option<TOut>.None;
    }

    /// <summary>
    /// Binds a <see cref="Option{TIn}"/> to a <see cref="Option{TOut}"/>.
    /// </summary>
    /// <param name="option">The <see cref="Option{TIn}"/> being mapped.</param>
    /// <param name="bindFunc">The bind function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Option{TOut}"/>.</returns>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static Option<TOut> Bind<TIn, TOut>(this Option<TIn> option, Func<TIn, Option<TOut>> bindFunc) where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(bindFunc);

        var (isSome, some) = option;
        return isSome
            ? bindFunc(some!)
            : Option<TOut>.None;
    }
}
