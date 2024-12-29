﻿namespace SleepingBear.Functional.Common;

/// <summary>
/// Unit class.
/// </summary>
public sealed record Unit
{
    private Unit()
    {
    }

    /// <summary>
    /// Static instance of <see cref="Unit"/>.
    /// </summary>
    public static readonly Unit Value = new();
}