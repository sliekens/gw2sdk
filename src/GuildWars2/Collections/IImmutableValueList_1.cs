using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace GuildWars2.Collections;

/// <summary>Represents an immutable list with value semantics for equality comparison.</summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
/// <remarks>
/// <para>This interface extends <see cref="IImmutableList{T}"/> with value-based equality and covariant return types.
/// The shadowed members return <see cref="IImmutableValueList{T}"/> instead of <see cref="IImmutableList{T}"/>,
/// satisfying the Liskov Substitution Principle: the more specific return type is still assignable to the base type.</para>
/// <para>C# does not support return type covariance on interface implementations directly,
/// so member shadowing with the <c>new</c> keyword is used to achieve the same effect.</para>
/// </remarks>
[CollectionBuilder(typeof(ImmutableValueList), nameof(ImmutableValueList.Create))]
[JsonConverter(typeof(ImmutableValueListJsonConverterFactory))]
public interface IImmutableValueList<T> : IImmutableList<T>, IEquatable<IImmutableValueList<T>>
{
    /// <inheritdoc cref="IImmutableList{T}.Add"/>
    new IImmutableValueList<T> Add(T value);

    /// <inheritdoc cref="IImmutableList{T}.AddRange"/>
    new IImmutableValueList<T> AddRange(IEnumerable<T> items);

    /// <inheritdoc cref="IImmutableList{T}.Clear"/>
    new IImmutableValueList<T> Clear();

    /// <inheritdoc cref="IImmutableList{T}.Insert"/>
    new IImmutableValueList<T> Insert(int index, T element);

    /// <inheritdoc cref="IImmutableList{T}.InsertRange"/>
    new IImmutableValueList<T> InsertRange(int index, IEnumerable<T> items);

    /// <inheritdoc cref="IImmutableList{T}.Remove"/>
    new IImmutableValueList<T> Remove(T value, IEqualityComparer<T>? equalityComparer);

    /// <inheritdoc cref="IImmutableList{T}.RemoveAll"/>
    new IImmutableValueList<T> RemoveAll(Predicate<T> match);

    /// <inheritdoc cref="IImmutableList{T}.RemoveAt"/>
    new IImmutableValueList<T> RemoveAt(int index);

    /// <inheritdoc cref="IImmutableList{T}.RemoveRange(IEnumerable{T}, IEqualityComparer{T}?)"/>
    new IImmutableValueList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer);

    /// <inheritdoc cref="IImmutableList{T}.RemoveRange(int, int)"/>
    new IImmutableValueList<T> RemoveRange(int index, int count);

    /// <inheritdoc cref="IImmutableList{T}.Replace"/>
    new IImmutableValueList<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer);

    /// <inheritdoc cref="IImmutableList{T}.SetItem"/>
    new IImmutableValueList<T> SetItem(int index, T value);
}
