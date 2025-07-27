using System.Diagnostics;

namespace GuildWars2.Collections;

/// <summary>
/// Represents a dictionary collection with value semantics, meaning two <see cref="ValueDictionary{TKey, TValue}"/> instances are considered equal if their contents are equal.
/// Value equality is determined by the keys and values in the dictionary, not by reference.
/// </summary>
/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
/// <remarks>
/// Adding items to the dictionary changes its hash code. <b>Do not</b> use this type in dictionaries or hash sets.
/// </remarks>
[PublicAPI]
[DebuggerDisplay("Count = {Count}")]
// Deriving from ImmutableDictionary would be preferable, but it is unavailable in .NET Standard 2.0.
public sealed class ValueDictionary<TKey, TValue> : Dictionary<TKey, TValue>,
    IEquatable<ValueDictionary<TKey, TValue>> where TKey : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDictionary{TKey, TValue}"/> class that is empty.
    /// </summary>
    public ValueDictionary()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDictionary{TKey, TValue}"/> class that is empty and uses the specified comparer.
    /// </summary>
    /// <param name="comparer">The comparer to use for the keys.</param>
    public ValueDictionary(IEqualityComparer<TKey>? comparer)
        : base(comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDictionary{TKey, TValue}"/> class that is empty and has the specified initial capacity.
    /// </summary>
    /// <param name="capacity">The number of elements that the new dictionary can initially store.</param>
    public ValueDictionary(int capacity)
        : base(capacity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDictionary{TKey, TValue}"/> class that is empty, has the specified initial capacity, and uses the specified comparer.
    /// </summary>
    /// <param name="capacity">The number of elements that the new dictionary can initially store.</param>
    /// <param name="comparer">The comparer to use for the keys.</param>
    public ValueDictionary(int capacity, IEqualityComparer<TKey>? comparer)
        : base(capacity, comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDictionary{TKey, TValue}"/> class that contains elements copied from the specified dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary whose elements are copied to the new dictionary.</param>
    public ValueDictionary(IDictionary<TKey, TValue> dictionary)
        : base(dictionary)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDictionary{TKey, TValue}"/> class that contains elements copied from the specified dictionary and uses the specified comparer.
    /// </summary>
    /// <param name="dictionary">The dictionary whose elements are copied to the new dictionary.</param>
    /// <param name="comparer">The comparer to use for the keys.</param>
    public ValueDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
        : base(dictionary, comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDictionary{TKey, TValue}"/> class that contains elements copied from the specified collection.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new dictionary.</param>
    public ValueDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
#if NET
        : base(collection)
#else
        : base(collection.ToDictionary(pair => pair.Key, pair => pair.Value))
#endif
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDictionary{TKey, TValue}"/> class that contains elements copied from the specified collection and uses the specified comparer.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new dictionary.</param>
    /// <param name="comparer">The comparer to use for the keys.</param>
    public ValueDictionary(
        IEnumerable<KeyValuePair<TKey, TValue>> collection,
        IEqualityComparer<TKey>? comparer
    )
#if NET
        : base(collection, comparer)
#else
        : base(collection.ToDictionary(pair => pair.Key, pair => pair.Value), comparer)
#endif
    {
    }

    /// <summary>
    /// Determines whether the current <see cref="ValueDictionary{TKey, TValue}"/> is equal to another <see cref="ValueDictionary{TKey, TValue}"/> based on value semantics.
    /// </summary>
    /// <param name="other">The other <see cref="ValueDictionary{TKey, TValue}"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the dictionaries are equal by value; otherwise, <c>false</c>.</returns>
    public bool Equals(ValueDictionary<TKey, TValue>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Count == other.Count
            && this.All(pair =>
                other.TryGetValue(pair.Key, out var otherValue)
                && EqualityComparer<TValue>.Default.Equals(pair.Value, otherValue)
            );
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj)
            || (obj is ValueDictionary<TKey, TValue> other && Equals(other));
    }

    /// <summary>
    /// Returns a hash code based on the keys and values in the dictionary.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var kvp in this.OrderBy(kv => kv.Key))
        {
            hash.Add(kvp.Key);
            hash.Add(kvp.Value);
        }

        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="ValueDictionary{TKey, TValue}"/> instances are equal by value.
    /// </summary>
    /// <param name="left">The first <see cref="ValueDictionary{TKey, TValue}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueDictionary{TKey, TValue}"/> to compare.</param>
    public static bool operator ==(
        ValueDictionary<TKey, TValue>? left,
        ValueDictionary<TKey, TValue>? right
    )
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="ValueDictionary{TKey, TValue}"/> instances are not equal by value.
    /// </summary>
    /// <param name="left">The first <see cref="ValueDictionary{TKey, TValue}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueDictionary{TKey, TValue}"/> to compare.</param>
    public static bool operator !=(
        ValueDictionary<TKey, TValue>? left,
        ValueDictionary<TKey, TValue>? right
    )
    {
        return !Equals(left, right);
    }
}
