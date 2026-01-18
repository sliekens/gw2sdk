namespace GuildWars2.Collections;

/// <summary>Represents an immutable set with value semantics for equality comparison.</summary>
/// <typeparam name="T">The type of elements in the set.</typeparam>
/// <remarks>
/// <para>This interface extends <see cref="IImmutableSet{T}"/> with value-based equality and covariant return types.
/// The shadowed members return <see cref="IImmutableValueSet{T}"/> instead of <see cref="IImmutableSet{T}"/>,
/// satisfying the Liskov Substitution Principle: the more specific return type is still assignable to the base type.</para>
/// <para>C# does not support return type covariance on interface implementations directly,
/// so member shadowing with the <c>new</c> keyword is used to achieve the same effect.</para>
/// </remarks>
public interface IImmutableValueSet<T> : IImmutableSet<T>, IEquatable<IImmutableValueSet<T>>
{
    /// <inheritdoc cref="IImmutableSet{T}.Add"/>
    new IImmutableValueSet<T> Add(T value);

    /// <inheritdoc cref="IImmutableSet{T}.Clear"/>
    new IImmutableValueSet<T> Clear();

    /// <inheritdoc cref="IImmutableSet{T}.Except"/>
    new IImmutableValueSet<T> Except(IEnumerable<T> other);

    /// <inheritdoc cref="IImmutableSet{T}.Intersect"/>
    new IImmutableValueSet<T> Intersect(IEnumerable<T> other);

    /// <inheritdoc cref="IImmutableSet{T}.Remove"/>
    new IImmutableValueSet<T> Remove(T value);

    /// <inheritdoc cref="IImmutableSet{T}.SymmetricExcept"/>
    new IImmutableValueSet<T> SymmetricExcept(IEnumerable<T> other);

    /// <inheritdoc cref="IImmutableSet{T}.Union"/>
    new IImmutableValueSet<T> Union(IEnumerable<T> other);
}
