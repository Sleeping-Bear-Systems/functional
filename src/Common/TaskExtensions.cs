﻿// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace SleepingBear.Functional.Common;

/// <summary>
///     Extension methods for <see cref="Task{TResult}" />.
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    ///     Lifts a value to a <see cref="Task{T}" />.
    /// </summary>
    public static Task<T> ToTask<T>(this T value)
    {
        return Task.FromResult(value);
    }
}