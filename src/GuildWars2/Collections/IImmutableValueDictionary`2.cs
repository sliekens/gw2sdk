namespace GuildWars2.Collections;

/// <summary>Represents an immutable dictionary with value semantics for equality comparison.</summary>
/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
/// <remarks>
/// <para>This interface extends <see cref="IImmutableDictionary{TKey, TValue}"/> with value-based equality and covariant return types.
/// The shadowed members return <see cref="IImmutableValueDictionary{TKey, TValue}"/> instead of <see cref="IImmutableDictionary{TKey, TValue}"/>,
/// satisfying the Liskov Substitution Principle: the more specific return type is still assignable to the base type.</para>
/// <para>C# does not support return type covariance on interface implementations directly,
/// so member shadowing with the <c>new</c> keyword is used to achieve the same effect.</para>
/// </remarks>
public interface IImmutableValueDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>, IEquatable<IImmutableValueDictionary<TKey, TValue>>
    where TKey : notnull
{
    /// <inheritdoc cref="IImmutableDictionary{TKey, TValue}.Add"/>
    new IImmutableValueDictionary<TKey, TValue> Add(TKey key, TValue value);

    /// <inheritdoc cref="IImmutableDictionary{TKey, TValue}.AddRange"/>
    new IImmutableValueDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

    /// <inheritdoc cref="IImmutableDictionary{TKey, TValue}.Clear"/>
    new IImmutableValueDictionary<TKey, TValue> Clear();

    /// <inheritdoc cref="IImmutableDictionary{TKey, TValue}.Remove"/>
    new IImmutableValueDictionary<TKey, TValue> Remove(TKey key);

    /// <inheritdoc cref="IImmutableDictionary{TKey, TValue}.RemoveRange"/>
    new IImmutableValueDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

    /// <inheritdoc cref="IImmutableDictionary{TKey, TValue}.SetItem"/>
    new IImmutableValueDictionary<TKey, TValue> SetItem(TKey key, TValue value);

    /// <inheritdoc cref="IImmutableDictionary{TKey, TValue}.SetItems"/>
    new IImmutableValueDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);
}
