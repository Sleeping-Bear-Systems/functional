using System.Diagnostics.CodeAnalysis;

namespace SleepingBear.Functional.Monads;

/// <summary>
///     Extension methods for <see cref="Option{T}" />.
/// </summary>
[SuppressMessage(category: "Naming", checkId: "CA1716:Identifiers should not match keywords")]
[SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
public static class OptionExtensions
{
    /// <summary>
    ///     Binds a <see cref="Option{TIn}" /> to a <see cref="Option{TOut}" />.
    /// </summary>
    /// <param name="option">The <see cref="Option{TIn}" /> being mapped.</param>
    /// <param name="bindFunc">The bind function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Option{TOut}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Option<TOut> Bind<TIn, TOut>(this Option<TIn> option, Func<TIn, Option<TOut>> bindFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(bindFunc);

        var (isSome, some) = option;
        return isSome
            ? bindFunc(some!)
            : Option<TOut>.None;
    }

    /// <summary>
    ///     Bind a <see cref="Option{T}" />.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Option<TOut> Bind<TIn, TOut>(
        this Option<TIn> option,
        Func<TIn, Option<TOut>> bindFunc,
        Func<Option<TOut>> bindNoneFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(bindFunc);
        ArgumentNullException.ThrowIfNull(bindNoneFunc);

        var (isSome, some) = option;

        return isSome
            ? bindFunc(some!)
            : bindNoneFunc();
    }

    /// <summary>
    ///     Binds a <see cref="Option{TIn}" /> to a <see cref="Option{TOut}" />.
    /// </summary>
    /// <param name="task">A <see cref="Task{TResult}" /> containing the <see cref="Option{TIn}" /> being mapped.</param>
    /// <param name="bindFunc">The bind function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Option{TOut}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Option<TOut>> BindAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, Option<TOut>> bindFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFunc);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome
            ? bindFunc(some!)
            : Option<TOut>.None;
    }

    /// <summary>
    ///     Binds a <see cref="Option{TIn}" /> to a <see cref="Option{TOut}" />.
    /// </summary>
    /// <param name="task">A <see cref="Task{TResult}" /> containing <see cref="Option{TIn}" /> being mapped.</param>
    /// <param name="bindFuncAsync">The bind function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Task{TResult}" /> containing the <see cref="Option{TOut}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Option<TOut>> BindAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, Task<Option<TOut>>> bindFuncAsync)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFuncAsync);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome
            ? await bindFuncAsync(some!).ConfigureAwait(continueOnCapturedContext: false)
            : Option<TOut>.None;
    }

    /// <summary>
    ///     Bind a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Option<TOut>> BindAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, Option<TOut>> bindFunc,
        Func<Option<TOut>> bindNoneFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFunc);
        ArgumentNullException.ThrowIfNull(bindNoneFunc);

        var option = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isSome, some) = option;

        return isSome
            ? bindFunc(some!)
            : bindNoneFunc();
    }

    /// <summary>
    ///     Bind a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Option<TOut>> BindAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, Task<Option<TOut>>> bindFuncAsync,
        Func<Task<Option<TOut>>> bindNoneFuncAsync)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(bindFuncAsync);
        ArgumentNullException.ThrowIfNull(bindNoneFuncAsync);

        var option = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isSome, some) = option;

        return isSome
            ? await bindFuncAsync(some!).ConfigureAwait(continueOnCapturedContext: false)
            : await bindNoneFuncAsync().ConfigureAwait(continueOnCapturedContext: false);
    }

    /// <summary>
    ///     Checks if the value of an <see cref="Option{T}" /> satisfies a predicate.
    /// </summary>
    public static Option<T> Check<T>(this Option<T> option, Func<T, bool> predicate)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return option.Bind(some => predicate(some) ? some : Option<T>.None);
    }

    /// <summary>
    ///     Checks if the value of an <see cref="Option{T}" /> does not satisfies a predicate.
    /// </summary>
    public static Option<T> CheckNot<T>(this Option<T> option, Func<T, bool> predicate)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return option.Bind(some => predicate(some) ? Option<T>.None : some);
    }

    /// <summary>
    ///     Maps a <see cref="Option{TIn}" /> to <see cref="Option{TOut}" />.
    /// </summary>
    /// <param name="option">The <see cref="Option{TIn}" /> being mapped.</param>
    /// <param name="mapFunc">The map function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Option{TOut}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Option<TOut> Map<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> mapFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(mapFunc);

        var (isSome, some) = option;
        return isSome
            ? new Option<TOut>(mapFunc(some!))
            : Option<TOut>.None;
    }

    /// <summary>
    ///     Maps a <see cref="Option{TIn}" /> to <see cref="Option{TOut}" /> asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Option{TIn}" /> being mapped.</param>
    /// <param name="mapFunc">The map function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Option{TOut}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Option<TOut>> MapAsync<TIn, TOut>(this Task<Option<TIn>> task, Func<TIn, TOut> mapFunc)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapFunc);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome
            ? new Option<TOut>(mapFunc(some!))
            : Option<TOut>.None;
    }

    /// <summary>
    ///     Maps a <see cref="Option{TIn}" /> to <see cref="Option{TOut}" /> asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Option{TIn}" /> being mapped.</param>
    /// <param name="mapFuncAsync">The map function.</param>
    /// <typeparam name="TIn">The type of the input lifted value.</typeparam>
    /// <typeparam name="TOut">The type of the output lifted value.</typeparam>
    /// <returns>A <see cref="Option{TOut}" />.</returns>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Option<TOut>> MapAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, Task<TOut>> mapFuncAsync)
        where TIn : notnull where TOut : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapFuncAsync);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome
            ? new Option<TOut>(await mapFuncAsync(some!).ConfigureAwait(continueOnCapturedContext: false))
            : Option<TOut>.None;
    }

    /// <summary>
    ///     Maps the none value of a <see cref="Option{T}" />.
    /// </summary>
    public static Option<T> MapNone<T>(
        this Option<T> option,
        Func<Option<T>> mapNoneFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(mapNoneFunc);

        return option.IsNone
            ? mapNoneFunc()
            : option;
    }

    /// <summary>
    ///     Maps the none value of a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    public static async Task<Option<TIn>> MapNoneAsync<TIn>(
        this Task<Option<TIn>> task,
        Func<Option<TIn>> mapNoneFunc)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapNoneFunc);

        var option = await task.ConfigureAwait(continueOnCapturedContext: false);
        return option.IsNone
            ? mapNoneFunc()
            : option;
    }

    /// <summary>
    ///     Maps the none value of a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    public static async Task<Option<TIn>> MapNoneAsync<TIn>(
        this Task<Option<TIn>> task,
        Func<Task<Option<TIn>>> mapNoneFuncAsync)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(mapNoneFuncAsync);

        var option = await task.ConfigureAwait(continueOnCapturedContext: false);
        return option.IsNone
            ? await mapNoneFuncAsync().ConfigureAwait(continueOnCapturedContext: false)
            : option;
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" />.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static T Match<T>(this Option<T> option, T noneValue) where T : notnull
    {
        var (isSome, some) = option;
        return isSome ? some! : noneValue;
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" />.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static TOut Match<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> someFunc, Func<TOut> noneFunc)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(someFunc);
        ArgumentNullException.ThrowIfNull(noneFunc);

        var (isSome, some) = option;
        return isSome
            ? someFunc(some!)
            : noneFunc();
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" />.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static TOut Match<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> someFunc, TOut none)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(someFunc);

        var (isSome, some) = option;
        return isSome
            ? someFunc(some!)
            : none;
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<T> MatchAsync<T>(this Task<Option<T>> task, T noneValue) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome ? some! : noneValue;
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, TOut> someFunc,
        Func<TOut> noneFunc) where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(someFunc);
        ArgumentNullException.ThrowIfNull(noneFunc);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome
            ? someFunc(some!)
            : noneFunc();
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, Task<TOut>> someFuncAsync,
        Func<Task<TOut>> noneFuncAsync) where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(someFuncAsync);
        ArgumentNullException.ThrowIfNull(noneFuncAsync);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome
            ? await someFuncAsync(some!).ConfigureAwait(continueOnCapturedContext: false)
            : await noneFuncAsync().ConfigureAwait(continueOnCapturedContext: false);
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, TOut> someFunc,
        TOut none) where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(someFunc);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome
            ? someFunc(some!)
            : none;
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" /> aynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, Task<TOut>> someFuncAsync,
        TOut none) where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(someFuncAsync);

        var (isSome, some) = await task.ConfigureAwait(continueOnCapturedContext: false);
        return isSome
            ? await someFuncAsync(some!).ConfigureAwait(continueOnCapturedContext: false)
            : none;
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" /> or throws an exception.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}" /> being matched.</param>
    /// <param name="noneFunc">The none function.</param>
    /// <typeparam name="T">The type of the lifted value.</typeparam>
    /// <returns>The lifted value.</returns>
    /// <exception cref="Exception">Thrown if the <see cref="Option{T}" /> is none.</exception>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static T MatchOrThrow<T>(
        this Option<T> option,
        Func<Exception> noneFunc)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(noneFunc);

        var (isSome, some) = option;
        return isSome
            ? some!
            : throw noneFunc();
    }

    /// <summary>
    ///     Matches a <see cref="Option{T}" /> or throws an exception.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}" /> being matched.</param>
    /// <param name="someFunc">The matching function.</param>
    /// <param name="noneFunc">The none function.</param>
    /// <typeparam name="TIn">The type of the lifted value.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <returns>The lifted value.</returns>
    /// <exception cref="Exception">Thrown if the <see cref="Option{T}" /> is none.</exception>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static TOut MatchOrThrow<TIn, TOut>(
        this Option<TIn> option,
        Func<TIn, TOut> someFunc,
        Func<Exception> noneFunc)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(someFunc);
        ArgumentNullException.ThrowIfNull(noneFunc);

        var (isSome, some) = option;
        return isSome
            ? someFunc(some!)
            : throw noneFunc();
    }

    /// <summary>
    ///     Matches a <see cref="Option{TIn}" /> or throws an exception.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing <see cref="Option{TIn}" /> being matched.</param>
    /// <param name="someFunc">The matching function.</param>
    /// <param name="noneFunc">The none function.</param>
    /// <typeparam name="TIn">The type of the lifted value.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <returns>The lifted value.</returns>
    /// <exception cref="Exception">Thrown if the <see cref="Option{TIn}" /> is none.</exception>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchOrThrowAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, TOut> someFunc,
        Func<Exception> noneFunc)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(someFunc);
        ArgumentNullException.ThrowIfNull(noneFunc);

        var option = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isSome, some) = option;
        return isSome
            ? someFunc(some!)
            : throw noneFunc();
    }

    /// <summary>
    ///     Matches a <see cref="Option{TIn}" /> or throws an exception.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing <see cref="Option{TIn}" /> being matched.</param>
    /// <param name="someFuncAsync">The matching function.</param>
    /// <param name="noneFuncAsync">The none function.</param>
    /// <typeparam name="TIn">The type of the lifted value.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <returns>The lifted value.</returns>
    /// <exception cref="Exception">Thrown if the <see cref="Option{TIn}" /> is none.</exception>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<TOut> MatchOrThrowAsync<TIn, TOut>(
        this Task<Option<TIn>> task,
        Func<TIn, Task<TOut>> someFuncAsync,
        Func<Task<Exception>> noneFuncAsync)
        where TIn : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(someFuncAsync);
        ArgumentNullException.ThrowIfNull(noneFuncAsync);

        var option = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isSome, some) = option;
        return isSome
            ? await someFuncAsync(some!).ConfigureAwait(continueOnCapturedContext: false)
            : throw await noneFuncAsync().ConfigureAwait(continueOnCapturedContext: false);
    }

    /// <summary>
    ///     Taps a <see cref="Option{T}" />.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static Option<T> Tap<T>(
        this Option<T> option,
        Action<T> someAction,
        Action? noneAction = null) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(someAction);

        var (isSome, some) = option;
        if (isSome)
        {
            someAction(some!);
        }
        else
        {
            noneAction?.Invoke();
        }

        return option;
    }

    /// <summary>
    ///     Taps a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Option<T>> TapAsync<T>(
        this Task<Option<T>> task,
        Action<T> someAction,
        Action? noneAction = null) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(someAction);

        var option = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isSome, some) = option;
        if (isSome)
        {
            someAction(some!);
        }
        else
        {
            noneAction?.Invoke();
        }

        return option;
    }

    /// <summary>
    ///     Tap a <see cref="Option{T}" /> asynchronously.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static async Task<Option<T>> TapAsync<T>(
        this Task<Option<T>> task,
        Func<T, Task> someFuncAsync,
        Func<Task>? noneFuncAsync = null) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(someFuncAsync);

        var option = await task.ConfigureAwait(continueOnCapturedContext: false);
        var (isSome, some) = option;
        if (isSome)
        {
            await someFuncAsync(some!).ConfigureAwait(continueOnCapturedContext: false);
        }
        else if (noneFuncAsync is not null)
        {
            await noneFuncAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        return option;
    }

    /// <summary>
    ///     Lifts a value to a <see cref="Option{T}" />.
    /// </summary>
    /// <param name="some">The value being lifted.</param>
    /// <typeparam name="T">The type of the value being lifted.</typeparam>
    /// <returns>A <see cref="Option{T}" />.</returns>
    public static Option<T> ToOption<T>(this T? some) where T : notnull
    {
        return some is null
            ? Option<T>.None
            : new Option<T>(some);
    }

    /// <summary>
    ///     Conditionally lifts a value to a <see cref="Option{T}" />.
    /// </summary>
    public static Option<T> ToOption<T>(this T? some, Func<T, bool> predicate) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return some is not null && predicate(some)
            ? new Option<T>(some)
            : Option<T>.None;
    }

    /// <summary>
    ///     Converts a <see cref="Task{T}" /> to a <see cref="Option{T}" />.
    /// </summary>
    public static async Task<Option<T>> ToOptionAsync<T>(this Task<T?> task) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(task);

        var some = await task.ConfigureAwait(continueOnCapturedContext: false);
        return some is null
            ? Option<T>.None
            : new Option<T>(some);
    }

    /// <summary>
    ///     Converts a <see cref="ValueTask{T}" /> to a <see cref="Option{T}" />.
    /// </summary>
    public static async Task<Option<T>> ToOptionAsync<T>(this ValueTask<T?> task) where T : notnull
    {
        var some = await task.ConfigureAwait(continueOnCapturedContext: false);
        return some is null
            ? Option<T>.None
            : new Option<T>(some);
    }

    /// <summary>
    ///     Tries to get the value of an <see cref="Option{T}" />.
    /// </summary>
    public static bool TrySome<T>(this Option<T> option, [NotNullWhen(returnValue: true)] out T? value)
        where T : notnull
    {
        var (isSome, some) = option;
        value = isSome ? some : default;
        return isSome;
    }
}