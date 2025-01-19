using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Validation;

/// <summary>
///     Extension methods for <see cref="decimal" />.
/// </summary>
public static class DecimalExtensions
{
    /// <summary>
    ///     Tries to convert object into a <see cref="decimal" />.
    /// </summary>
    public static Option<decimal> AsDecimal(this object? value)
    {
        return value switch
        {
            decimal d => d,
            int d => d,
            long d => d,
            string d => decimal.TryParse(d, out var result) ? result : Option<decimal>.None,
            _ => Option<decimal>.None
        };
    }
}