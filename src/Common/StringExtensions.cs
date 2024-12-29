namespace SleepingBear.Functional.Common;

/// <summary>
/// Extensions for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Returns a default value if the string is null.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string IfNull(this string? value, string? defaultValue = null)
    {
        return value ?? defaultValue ?? string.Empty;
    }
}
