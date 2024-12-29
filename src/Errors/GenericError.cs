// ReSharper disable NotAccessedPositionalProperty.Global

namespace SleepingBear.Functional.Errors;

/// <summary>
/// Generic Error.
/// </summary>
/// <param name="Value">The value wrapped by the error.</param>
/// <typeparam name="T">The type of the wrapped value.</typeparam>
public sealed record GenericError<T>(T Value) : Error where T : notnull;
