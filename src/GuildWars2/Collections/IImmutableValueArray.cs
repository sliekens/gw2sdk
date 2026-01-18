using System.Runtime.CompilerServices;

namespace GuildWars2.Collections;

/// <summary>Represents an immutable array with value semantics for equality comparison.</summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
/// <remarks>
/// <para>This interface extends <see cref="IImmutableList{T}"/> with value-based equality and covariant return types.
/// The shadowed members return <see cref="IImmutableValueArray{T}"/> instead of <see cref="IImmutableList{T}"/>,
/// satisfying the Liskov Substitution Principle: the more specific return type is still assignable to the base type.</para>
/// <para>C# does not support return type covariance on interface implementations directly,
/// so member shadowing with the <c>new</c> keyword is used to achieve the same effect.</para>
/// </remarks>
[CollectionBuilder(typeof(ImmutableValueArray), nameof(ImmutableValueArray.Create))]
public interface IImmutableValueArray<T> : IImmutableList<T>, IEquatable<IImmutableValueArray<T>>
{
    /// <inheritdoc cref="IImmutableList{T}.Add"/>
    new IImmutableValueArray<T> Add(T value);

    /// <inheritdoc cref="IImmutableList{T}.AddRange"/>
    new IImmutableValueArray<T> AddRange(IEnumerable<T> items);

    /// <inheritdoc cref="IImmutableList{T}.Clear"/>
    new IImmutableValueArray<T> Clear();

    /// <inheritdoc cref="IImmutableList{T}.Insert"/>
    new IImmutableValueArray<T> Insert(int index, T element);

    /// <inheritdoc cref="IImmutableList{T}.InsertRange"/>
    new IImmutableValueArray<T> InsertRange(int index, IEnumerable<T> items);

    /// <inheritdoc cref="IImmutableList{T}.Remove"/>
    new IImmutableValueArray<T> Remove(T value, IEqualityComparer<T>? equalityComparer);

    /// <inheritdoc cref="IImmutableList{T}.RemoveAll"/>
    new IImmutableValueArray<T> RemoveAll(Predicate<T> match);

    /// <inheritdoc cref="IImmutableList{T}.RemoveAt"/>
    new IImmutableValueArray<T> RemoveAt(int index);

    /// <inheritdoc cref="IImmutableList{T}.RemoveRange(IEnumerable{T}, IEqualityComparer{T}?)"/>
    new IImmutableValueArray<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer);

    /// <inheritdoc cref="IImmutableList{T}.RemoveRange(int, int)"/>
    new IImmutableValueArray<T> RemoveRange(int index, int count);

    /// <inheritdoc cref="IImmutableList{T}.Replace"/>
    new IImmutableValueArray<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer);

    /// <inheritdoc cref="IImmutableList{T}.SetItem"/>
    new IImmutableValueArray<T> SetItem(int index, T value);
}
