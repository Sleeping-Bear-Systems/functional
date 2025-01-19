// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global

namespace SleepingBear.Functional.Common;

/// <summary>
///     Extensions methods for piping.
/// </summary>
public static class PipeExtensions
{
    /// <summary>
    ///     Pipes a value.
    /// </summary>
    public static TOut Pipe<TIn, TOut>(this TIn value, Func<TIn, TOut> func)
    {
        ArgumentNullException.ThrowIfNull(func);

        return func(value);
    }

    /// <summary>
    ///     Pipes a value asynchronously.
    /// </summary>
    public static async Task<TOut> PipeAsync<TIn, TOut>(this Task<TIn> task, Func<TIn, TOut> func)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return func(value);
    }

    /// <summary>
    ///     Pipes a value asynchronously.
    /// </summary>
    public static async Task<TOut> PipeAsync<TIn, TOut>(this Task<TIn> task, Func<TIn, Task<TOut>> func)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return await func(value).ConfigureAwait(continueOnCapturedContext: false);
    }

    /// <summary>
    ///     Executes a pipe function conditionally.
    /// </summary>
    /// <param name="value">The value being piped.</param>
    /// <param name="condition">The condition.</param>
    /// <param name="func">The pipe function.</param>
    /// <typeparam name="TIn">The type of the piped value.</typeparam>
    /// <returns>The value of the pipe func if true or the value otherwise.</returns>
    public static TIn PipeIf<TIn>(this TIn value, bool condition, Func<TIn, TIn> func)
    {
        ArgumentNullException.ThrowIfNull(func);

        return condition
            ? func(value)
            : value;
    }

    /// <summary>
    ///     Executes a pipe function conditionally.
    /// </summary>
    /// <param name="value">The value being piped.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="func">The pipe function.</param>
    /// <typeparam name="TIn">The type of the piped value.</typeparam>
    /// <returns>The value of the pipe func if true or the value otherwise.</returns>
    public static TIn PipeIf<TIn>(this TIn value, Func<TIn, bool> predicate, Func<TIn, TIn> func)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(func);

        return value.PipeIf(predicate(value), func);
    }

    /// <summary>
    ///     Executes a pipe function conditionally asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being piped.</param>
    /// <param name="condition">The condition.</param>
    /// <param name="func">The pipe function.</param>
    /// <typeparam name="TIn">The type of the piped value.</typeparam>
    /// <returns>The value of the pipe func if true or the value otherwise.</returns>
    public static async Task<TIn> PipeIfAsync<TIn>(this Task<TIn> task, bool condition, Func<TIn, TIn> func)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return condition
            ? func(value)
            : value;
    }

    /// <summary>
    ///     Executes a pipe function conditionally asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being piped.</param>
    /// <param name="condition">The condition.</param>
    /// <param name="func">The pipe function.</param>
    /// <typeparam name="TIn">The type of the piped value.</typeparam>
    /// <returns>The value of the pipe func if true or the value otherwise.</returns>
    public static async Task<TIn> PipeIfAsync<TIn>(this Task<TIn> task, bool condition, Func<TIn, Task<TIn>> func)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return condition
            ? await func(value).ConfigureAwait(continueOnCapturedContext: false)
            : value;
    }

    /// <summary>
    ///     Executes a pipe function conditionally asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being piped.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="func">The pipe function.</param>
    /// <typeparam name="TIn">The type of the piped value.</typeparam>
    /// <returns>The value of the pipe func if true or the value otherwise.</returns>
    public static async Task<TIn> PipeIfAsync<TIn>(
        this Task<TIn> task,
        Func<TIn, bool> predicate,
        Func<TIn, TIn> func)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return predicate(value)
            ? func(value)
            : value;
    }

    /// <summary>
    ///     Executes a pipe function conditionally asynchronously.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}" /> containing value being piped.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="func">The pipe function.</param>
    /// <typeparam name="TIn">The type of the piped value.</typeparam>
    /// <returns>The value of the pipe func if true or the value otherwise.</returns>
    public static async Task<TIn> PipeIfAsync<TIn>(
        this Task<TIn> task,
        Func<TIn, Task<bool>> predicate,
        Func<TIn, Task<TIn>> func)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        return await predicate(value).ConfigureAwait(continueOnCapturedContext: false)
            ? await func(value).ConfigureAwait(continueOnCapturedContext: false)
            : value;
    }

    /// <summary>
    ///     Executes a pipe function if the function is not null.
    /// </summary>
    /// <param name="value">The value being piped.</param>
    /// <param name="func">The pipe function. (optional)</param>
    /// <typeparam name="TIn">The type of the piped value.</typeparam>
    /// <returns>The piped value if the pipe function is null or the result of the pipe function otherwise.</returns>
    public static TIn PipeIfNotNull<TIn>(this TIn value, Func<TIn, TIn>? func)
    {
        return func is null
            ? value
            : func(value);
    }

    /// <summary>
    ///     Taps a value.
    /// </summary>
    public static TIn Tap<TIn>(this TIn value, Action<TIn> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        action(value);
        return value;
    }

    /// <summary>
    ///     Taps a value asynchronously.
    /// </summary>
    public static async Task<TIn> TapAsync<TIn>(this Task<TIn> task, Action<TIn> action)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(action);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        action(value);
        return value;
    }

    /// <summary>
    ///     Taps a value asynchronously.
    /// </summary>
    public static async Task<TIn> TapAsync<TIn>(this Task<TIn> task, Func<TIn, Task> func)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(continueOnCapturedContext: false);
        await func(value).ConfigureAwait(continueOnCapturedContext: false);
        return value;
    }
}