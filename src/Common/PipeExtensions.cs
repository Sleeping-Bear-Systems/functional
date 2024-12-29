// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace SleepingBear.Functional.Common;

/// <summary>
/// Extensions methods for piping.
/// </summary>
public static class PipeExtensions
{
    /// <summary>
    /// Pipes a value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="func"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    public static TOut Pipe<TIn, TOut>(this TIn value, Func<TIn, TOut> func)
    {
        ArgumentNullException.ThrowIfNull(func);
        
        return func(value);
    }
    
    /// <summary>
    /// Pipes a value asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="func"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    public static async Task<TOut> PipeAsync<TIn, TOut>(this Task<TIn> task, Func<TIn, TOut> func)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(false);
        return func(value);
    }
    
    /// <summary>
    /// Pipes a value asynchronously.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="func"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    public static async Task<TOut> PipeAsync<TIn, TOut>(this Task<TIn> task, Func<TIn, Task<TOut>> func)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(func);

        var value = await task.ConfigureAwait(false);
        return await func(value).ConfigureAwait(false);
    }

}