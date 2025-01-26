namespace GuildWars2.Collections;

// WARNING: adding items to the set changes its hash code,
// DO NOT use this type in dictionaries or hash sets.
// I would have liked to use ImmutableHashSet instead, but it's
// unavailable in .NET Standard 2.0.
internal sealed class ValueHashSet<T> : HashSet<T>, IEquatable<ValueHashSet<T>>
{
    public ValueHashSet()
    {
    }

#if NET
    public ValueHashSet(int capacity)
        : base(capacity)
    {
    }
#endif

    public ValueHashSet(IEnumerable<T> collection)
        : base(collection)
    {
    }

    public bool Equals(ValueHashSet<T>? other)
    {
        if (other is null) return false;
        return ReferenceEquals(this, other) || SetEquals(other);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ValueHashSet<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var item in this)
        {
            hash.Add(item);
        }

        return hash.ToHashCode();
    }

    public static bool operator ==(ValueHashSet<T>? left, ValueHashSet<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueHashSet<T>? left, ValueHashSet<T>? right)
    {
        return !Equals(left, right);
    }
}
