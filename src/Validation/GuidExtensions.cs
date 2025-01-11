using SleepingBear.Functional.Errors;
using SleepingBear.Functional.Monads;

namespace SleepingBear.Functional.Validation;

/// <summary>
///     Extension methods for <see cref="Guid" />.
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    ///     Checks if the lifted <see cref="Guid" /> is not empty.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="errorFunc"></param>
    /// <returns></returns>
    public static Result<Guid> CheckIsNotEmpty(this Result<Guid> result, Func<Error> errorFunc)
    {
        return result.Bind<Guid, Guid>(ok => ok == Guid.Empty ? errorFunc() : ok);
    }

    /// <summary>
    ///     Checks if the lifted <see cref="Guid" /> is not empty.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<Guid> CheckIsNotEmpty(this Result<Guid> result, Error error)
    {
        return result.Bind<Guid, Guid>(ok => ok == Guid.Empty ? error : ok);
    }

    /// <summary>
    ///     Checks if the lifted <see cref="Guid" /> is not empty.
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static Option<Guid> CheckIsNotEmpty(this Option<Guid> option)
    {
        return option.Bind(some => some == Guid.Empty ? Option<Guid>.None : some);
    }
}