namespace SleepingBear.Functional.Errors;

/// <summary>
///     Value Error.
/// </summary>
/// <param name="Value">The value wrapped by the error.</param>
/// <typeparam name="T">The type of the wrapped value.</typeparam>
public sealed record ValueError<T>(T Value) : Error where T : notnull;