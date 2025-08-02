using System.Diagnostics;

namespace GuildWars2.Collections;

/// <summary>
/// Represents a set collection with value semantics, meaning two <see cref="ValueHashSet{T}"/> instances are considered equal if their contents are equal.
/// Value equality is determined by the set of items in the collection, not by reference.
/// </summary>
/// <typeparam name="T">The type of elements in the set.</typeparam>
/// <remarks>
/// Adding items to the set changes its hash code. <b>Do not</b> use this type in dictionaries or hash sets.
/// </remarks>
[PublicAPI]
[DebuggerDisplay("Count = {Count}")]
// Deriving from ImmutableHashSet would be preferable, but it is unavailable in .NET Standard 2.0.
public sealed class ValueHashSet<T> : HashSet<T>, IEquatable<ValueHashSet<T>>
{
    /// <summary>Initializes a new instance of the <see cref="ValueHashSet{T}"/> class that is empty.</summary>
    public ValueHashSet()
    {
    }

#if NET
    /// <summary>Initializes a new instance of the <see cref="ValueHashSet{T}"/> class that is empty and has the specified initial capacity.</summary>
    /// <param name="capacity">The number of elements that the new set can initially store.</param>
    public ValueHashSet(int capacity)
        : base(capacity)
    {
    }
#endif

    /// <summary>Initializes a new instance of the <see cref="ValueHashSet{T}"/> class that contains elements copied from the specified collection.</summary>
    /// <param name="collection">The collection whose elements are copied to the new set.</param>
    public ValueHashSet(IEnumerable<T> collection)
        : base(collection)
    {
    }

    /// <summary>Determines whether the current <see cref="ValueHashSet{T}"/> is equal to another <see cref="ValueHashSet{T}"/> based on value semantics.</summary>
    /// <param name="other">The other <see cref="ValueHashSet{T}"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the sets are equal by value; otherwise, <c>false</c>.</returns>
    public bool Equals(ValueHashSet<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || SetEquals(other);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is ValueHashSet<T> other && Equals(other));
    }

    /// <summary>Returns a hash code based on the values of the items in the set.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        HashCode hash = new();
        foreach (var item in this)
        {
            hash.Add(item);
        }

        return hash.ToHashCode();
    }

    /// <summary>Determines whether two <see cref="ValueHashSet{T}"/> instances are equal by value.</summary>
    /// <param name="left">The first <see cref="ValueHashSet{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueHashSet{T}"/> to compare.</param>
    /// <returns><c>true</c> if the sets are equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ValueHashSet<T>? left, ValueHashSet<T>? right)
    {
        return Equals(left, right);
    }

    /// <summary>Determines whether two <see cref="ValueHashSet{T}"/> instances are not equal by value.</summary>
    /// <param name="left">The first <see cref="ValueHashSet{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueHashSet{T}"/> to compare.</param>
    /// <returns><c>true</c> if the sets are not equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ValueHashSet<T>? left, ValueHashSet<T>? right)
    {
        return !Equals(left, right);
    }
}
