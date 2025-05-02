using System.Diagnostics;

namespace GuildWars2.Collections;

// WARNING: adding items to the dictionary changes its hash code,
// DO NOT use this type in dictionaries or hash sets.
// I would have liked to use ImmutableDictionary instead, but it's
// unavailable in .NET Standard 2.0.
[DebuggerDisplay("Count = {Count}")]
internal sealed class ValueDictionary<TKey, TValue> : Dictionary<TKey, TValue>,
    IEquatable<ValueDictionary<TKey, TValue>> where TKey : notnull
{
    public ValueDictionary()
    {
    }

    public ValueDictionary(IEqualityComparer<TKey>? comparer)
        : base(comparer)
    {
    }

    public ValueDictionary(int capacity)
        : base(capacity)
    {
    }

    public ValueDictionary(int capacity, IEqualityComparer<TKey>? comparer)
        : base(capacity, comparer)
    {
    }

    public ValueDictionary(IDictionary<TKey, TValue> dictionary)
        : base(dictionary)
    {
    }

    public ValueDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
        : base(dictionary, comparer)
    {
    }

    public ValueDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
#if NET
        : base(collection)
#else
        : base(collection.ToDictionary(pair => pair.Key, pair => pair.Value))
#endif
    {
    }

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

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj)
            || (obj is ValueDictionary<TKey, TValue> other && Equals(other));
    }

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

    public static bool operator ==(
        ValueDictionary<TKey, TValue>? left,
        ValueDictionary<TKey, TValue>? right
    )
    {
        return Equals(left, right);
    }

    public static bool operator !=(
        ValueDictionary<TKey, TValue>? left,
        ValueDictionary<TKey, TValue>? right
    )
    {
        return !Equals(left, right);
    }
}
