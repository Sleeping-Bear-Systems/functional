using System.Diagnostics.CodeAnalysis;

namespace SleepingBear.Functional.Monads;

/// <summary>
/// Monad representing an optional value.
/// </summary>
/// <typeparam name="T">The type of the lifted value.</typeparam>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
public readonly record struct Option<T> where T : notnull
{
    private readonly T? _some;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Option()
    {
        this._some = default;
        this.IsSome = false;
    }

    internal Option(T some)
    {
        this._some = some;
        this.IsSome = true;
    }

    /// <summary>
    /// Flag indicating the monad contains a value.
    /// </summary>
    public bool IsSome { get; }

    /// <summary>
    /// Flag indicating the monad does not contain a value.
    /// </summary>
    public bool IsNone => !this.IsSome;

    /// <summary>
    /// None instance.
    /// </summary>
    public static readonly Option<T> None = new();

    /// <summary>
    /// Deconstructs the monad.
    /// </summary>
    /// <param name="isSome">The flag indicating the monad contains a value.</param>
    /// <param name="some">The lifted value.</param>
    public void Deconstruct(out bool isSome, out T? some)
    {
        isSome = this.IsSome;
        some = this._some;
    }
}
