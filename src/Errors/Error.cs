using System.Diagnostics.CodeAnalysis;

namespace SleepingBear.Functional.Errors;

/// <summary>
///     Base class for all error types.
/// </summary>
[SuppressMessage(category: "Naming", checkId: "CA1716:Identifiers should not match keywords")]
public abstract record Error;