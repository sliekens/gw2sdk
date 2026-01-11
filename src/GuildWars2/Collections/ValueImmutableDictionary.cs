using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace GuildWars2.Collections;

/// <summary>Represents an immutable dictionary collection with value semantics, meaning two <see cref="ValueImmutableDictionary{TKey, TValue}"/> instances are considered equal if their contents are equal.</summary>
/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
[DebuggerDisplay("Count = {Count}")]
[SuppressMessage("Style", "IDE0028", Justification = "Cannot simplify constructor calls that wrap ImmutableDictionary<TKey, TValue>.")]
[SuppressMessage("Style", "IDE0301", Justification = "Cannot simplify to collection expression.")]
[SuppressMessage("Style", "IDE0303", Justification = "Cannot simplify to collection expression.")]
public sealed class ValueImmutableDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>, IEquatable<ValueImmutableDictionary<TKey, TValue>>
    where TKey : notnull
{
    /// <summary>Gets an empty <see cref="ValueImmutableDictionary{TKey, TValue}"/>.</summary>
    [SuppressMessage("Design", "CA1000", Justification = "Follows BCL pattern for immutable collections.")]
    public static ValueImmutableDictionary<TKey, TValue> Empty { get; } = new();

    private readonly ImmutableDictionary<TKey, TValue> items;

    /// <summary>Initializes a new instance of the <see cref="ValueImmutableDictionary{TKey, TValue}"/> class that is empty.</summary>
    public ValueImmutableDictionary()
    {
        items = ImmutableDictionary<TKey, TValue>.Empty;
    }

    /// <summary>Initializes a new instance of the <see cref="ValueImmutableDictionary{TKey, TValue}"/> class that contains elements copied from the specified collection.</summary>
    /// <param name="collection">The collection whose elements are copied to the new dictionary.</param>
    public ValueImmutableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
    {
        items = ImmutableDictionary.CreateRange(collection);
    }

    private ValueImmutableDictionary(ImmutableDictionary<TKey, TValue> items)
    {
        this.items = items;
    }

    /// <inheritdoc/>
    public int Count => items.Count;

    /// <inheritdoc/>
    public IEnumerable<TKey> Keys => items.Keys;

    /// <inheritdoc/>
    public IEnumerable<TValue> Values => items.Values;

    /// <inheritdoc/>
    public TValue this[TKey key] => items[key];

    /// <inheritdoc/>
    public bool ContainsKey(TKey key)
    {
        return items.ContainsKey(key);
    }

    /// <inheritdoc/>
#if NET
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
#else
    public bool TryGetValue(TKey key, out TValue value)
#endif
    {
        return items.TryGetValue(key, out value!);
    }

    /// <summary>Creates a new <see cref="ValueImmutableDictionary{TKey, TValue}"/> with the specified key and value added or updated.</summary>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The value to associate with the key.</param>
    /// <returns>A new dictionary with the key-value pair added or updated.</returns>
    public ValueImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
    {
        return new ValueImmutableDictionary<TKey, TValue>(items.SetItem(key, value));
    }

    /// <summary>Creates a new <see cref="ValueImmutableDictionary{TKey, TValue}"/> with the specified key and value added.</summary>
    /// <param name="key">The key to add.</param>
    /// <param name="value">The value to associate with the key.</param>
    /// <returns>A new dictionary with the key-value pair added.</returns>
    /// <exception cref="ArgumentException">The key already exists in the dictionary.</exception>
    public ValueImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
    {
        return new ValueImmutableDictionary<TKey, TValue>(items.Add(key, value));
    }

    /// <summary>Creates a new <see cref="ValueImmutableDictionary{TKey, TValue}"/> with the specified key-value pairs added.</summary>
    /// <param name="pairs">The key-value pairs to add.</param>
    /// <returns>A new dictionary with the key-value pairs added.</returns>
    public ValueImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
    {
        return new ValueImmutableDictionary<TKey, TValue>(items.AddRange(pairs));
    }

    /// <summary>Creates a new <see cref="ValueImmutableDictionary{TKey, TValue}"/> with the specified key removed.</summary>
    /// <param name="key">The key to remove.</param>
    /// <returns>A new dictionary with the key removed, or the same dictionary if the key was not found.</returns>
    public ValueImmutableDictionary<TKey, TValue> Remove(TKey key)
    {
        ImmutableDictionary<TKey, TValue> newItems = items.Remove(key);
        return ReferenceEquals(newItems, items) ? this : new ValueImmutableDictionary<TKey, TValue>(newItems);
    }

    /// <summary>Creates a new <see cref="ValueImmutableDictionary{TKey, TValue}"/> with the specified keys removed.</summary>
    /// <param name="keys">The keys to remove.</param>
    /// <returns>A new dictionary with the keys removed.</returns>
    public ValueImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
    {
        return new ValueImmutableDictionary<TKey, TValue>(items.RemoveRange(keys));
    }

    /// <summary>Creates a new <see cref="ValueImmutableDictionary{TKey, TValue}"/> with all items removed.</summary>
    /// <returns>An empty dictionary.</returns>
    public ValueImmutableDictionary<TKey, TValue> Clear()
    {
        return Empty;
    }

    /// <summary>Determines whether the current <see cref="ValueImmutableDictionary{TKey, TValue}"/> is equal to another <see cref="ValueImmutableDictionary{TKey, TValue}"/> based on value semantics.</summary>
    /// <param name="other">The other <see cref="ValueImmutableDictionary{TKey, TValue}"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the dictionaries are equal by value; otherwise, <c>false</c>.</returns>
    public bool Equals(ValueImmutableDictionary<TKey, TValue>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (Count != other.Count)
        {
            return false;
        }

        foreach (KeyValuePair<TKey, TValue> pair in items)
        {
            if (!other.items.TryGetValue(pair.Key, out TValue? otherValue)
                || !EqualityComparer<TValue>.Default.Equals(pair.Value, otherValue))
            {
                return false;
            }
        }

        return true;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is ValueImmutableDictionary<TKey, TValue> other && Equals(other));
    }

    /// <summary>Returns a hash code based on the keys and values in the dictionary.</summary>
    /// <returns>A hash code for the current object.</returns>
    /// <remarks>Uses XOR to combine hash codes so that order does not affect the result.</remarks>
    public override int GetHashCode()
    {
        int hash = 0;
        foreach (KeyValuePair<TKey, TValue> kvp in items)
        {
            hash ^= HashCode.Combine(kvp.Key, kvp.Value);
        }

        return hash;
    }

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Determines whether two <see cref="ValueImmutableDictionary{TKey, TValue}"/> instances are equal by value.</summary>
    /// <param name="left">The first <see cref="ValueImmutableDictionary{TKey, TValue}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueImmutableDictionary{TKey, TValue}"/> to compare.</param>
    /// <returns><c>true</c> if the dictionaries are equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ValueImmutableDictionary<TKey, TValue>? left, ValueImmutableDictionary<TKey, TValue>? right)
    {
        return Equals(left, right);
    }

    /// <summary>Determines whether two <see cref="ValueImmutableDictionary{TKey, TValue}"/> instances are not equal by value.</summary>
    /// <param name="left">The first <see cref="ValueImmutableDictionary{TKey, TValue}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueImmutableDictionary{TKey, TValue}"/> to compare.</param>
    /// <returns><c>true</c> if the dictionaries are not equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ValueImmutableDictionary<TKey, TValue>? left, ValueImmutableDictionary<TKey, TValue>? right)
    {
        return !Equals(left, right);
    }

    /// <summary>Determines whether the dictionary contains the specified key-value pair.</summary>
    /// <param name="pair">The key-value pair to locate.</param>
    /// <returns><c>true</c> if the dictionary contains the specified pair; otherwise, <c>false</c>.</returns>
    public bool Contains(KeyValuePair<TKey, TValue> pair)
    {
        return items.Contains(pair);
    }

    /// <summary>Creates a new <see cref="ValueImmutableDictionary{TKey, TValue}"/> with the specified key-value pairs set, adding or updating as necessary.</summary>
    /// <param name="items">The key-value pairs to set.</param>
    /// <returns>A new dictionary with the key-value pairs set.</returns>
    public ValueImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
        return new ValueImmutableDictionary<TKey, TValue>(this.items.SetItems(items));
    }

    /// <summary>Searches the dictionary for a given key and returns the equal key it finds, if any.</summary>
    /// <param name="equalKey">The key to search for.</param>
    /// <param name="actualKey">The key from the dictionary that the search found, or <paramref name="equalKey"/> if the search yielded no match.</param>
    /// <returns><c>true</c> if the search was successful; otherwise, <c>false</c>.</returns>
    public bool TryGetKey(TKey equalKey, out TKey actualKey)
    {
        return items.TryGetKey(equalKey, out actualKey);
    }

    #region Explicit IImmutableDictionary<TKey, TValue> implementation

    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
        return Add(key, value);
    }

    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
    {
        return AddRange(pairs);
    }

    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
    {
        return Clear();
    }

    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
    {
        return Remove(key);
    }

    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
    {
        return RemoveRange(keys);
    }

    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
    {
        return SetItem(key, value);
    }

    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
        return SetItems(items);
    }

    #endregion Explicit IImmutableDictionary<TKey, TValue> implementation
}
