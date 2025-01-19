using System.Text;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace SleepingBear.Functional.Common;

/// <summary>
///     Extension methods for <see cref="StringBuilder" />.
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>
    ///     Appends conditionally to a <see cref="StringBuilder" />.
    /// </summary>
    public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string value)
    {
        ArgumentNullException.ThrowIfNull(builder);

        if (condition)
        {
            builder.Append(value);
        }

        return builder;
    }

    /// <summary>
    ///     Appends conditionally to a <see cref="StringBuilder" />.
    /// </summary>
    public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string trueValue,
        string falseValue)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.Append(condition ? trueValue : falseValue);
    }

    /// <summary>
    ///     Appends conditionally to a <see cref="StringBuilder" />.
    /// </summary>
    public static StringBuilder AppendIf(this StringBuilder builder, Func<bool> predicate, string value)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return builder.AppendIf(predicate(), value);
    }

    /// <summary>
    ///     Appends conditionally to a <see cref="StringBuilder" />.
    /// </summary>
    public static StringBuilder AppendIf(this StringBuilder builder, Func<bool> predicate, string trueValue,
        string falseValue)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return builder.AppendIf(predicate(), trueValue, falseValue);
    }

    /// <summary>
    ///     Appends line conditionally to a <see cref="StringBuilder" />.
    /// </summary>
    public static StringBuilder AppendLineIf(this StringBuilder builder, bool condition, string value)
    {
        ArgumentNullException.ThrowIfNull(builder);

        if (condition)
        {
            builder.AppendLine(value);
        }

        return builder;
    }

    /// <summary>
    ///     Appends line conditionally to a <see cref="StringBuilder" />.
    /// </summary>
    public static StringBuilder AppendLineIf(this StringBuilder builder, bool condition, string trueValue,
        string falseValue)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AppendLine(condition ? trueValue : falseValue);
    }

    /// <summary>
    ///     Appends line conditionally to a <see cref="StringBuilder" />.
    /// </summary>
    public static StringBuilder AppendLineIf(this StringBuilder builder, Func<bool> predicate, string value)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return builder.AppendLineIf(predicate(), value);
    }

    /// <summary>
    ///     Appends line conditionally to a <see cref="StringBuilder" />.
    /// </summary>
    public static StringBuilder AppendLineIf(this StringBuilder builder, Func<bool> predicate, string trueValue,
        string falseValue)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return builder.AppendLineIf(predicate(), trueValue, falseValue);
    }
}