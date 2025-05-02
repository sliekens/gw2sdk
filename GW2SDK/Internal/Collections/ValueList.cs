using System.Diagnostics;

namespace GuildWars2.Collections;

// WARNING: adding items to the list changes its hash code,
// DO NOT use this type in dictionaries or hash sets.
// I would have liked to use ImmutableList instead, but it's
// unavailable in .NET Standard 2.0.
[DebuggerDisplay("Count = {Count}")]
internal sealed class ValueList<T> : List<T>, IEquatable<ValueList<T>>
{
    public ValueList()
    {
    }

    public ValueList(int capacity)
        : base(capacity)
    {
    }

    public ValueList(IEnumerable<T> collection)
        : base(collection)
    {
    }

    public bool Equals(ValueList<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Count == other.Count && this.SequenceEqual(other);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is ValueList<T> other && Equals(other));
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

    public static bool operator ==(ValueList<T>? left, ValueList<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueList<T>? left, ValueList<T>? right)
    {
        return !Equals(left, right);
    }
}
