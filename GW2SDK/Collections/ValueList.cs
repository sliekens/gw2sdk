using System.Diagnostics;

namespace GuildWars2.Collections;

/// <summary>
/// Represents a list collection with value semantics, meaning two <see cref="ValueList{T}"/> instances are considered equal if their contents are equal.
/// Value equality is determined by the sequence and values of the items in the collection, not by reference.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
/// <remarks>
/// Adding items to the list changes its hash code. <b>Do not</b> use this type in dictionaries or hash sets.
/// </remarks>
[DebuggerDisplay("Count = {Count}")]
// Deriving from ImmutableList would be preferable, but it is unavailable in .NET Standard 2.0.
public sealed class ValueList<T> : List<T>, IEquatable<ValueList<T>>
{
    /// <summary>Initializes a new instance of the <see cref="ValueList{T}"/> class that is empty.</summary>
    public ValueList()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ValueList{T}"/> class that is empty and has the specified initial capacity.</summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public ValueList(int capacity)
        : base(capacity)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ValueList{T}"/> class that contains elements copied from the specified collection.</summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public ValueList(IEnumerable<T> collection)
        : base(collection)
    {
    }

    /// <summary>Determines whether the current <see cref="ValueList{T}"/> is equal to another <see cref="ValueList{T}"/> based on value semantics.</summary>
    /// <param name="other">The other <see cref="ValueList{T}"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the lists are equal by value; otherwise, <c>false</c>.</returns>
    public bool Equals(ValueList<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Count == other.Count && this.SequenceEqual(other);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is ValueList<T> other && Equals(other));
    }

    /// <summary>Returns a hash code based on the values of the items in the list.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        HashCode hash = new();
        foreach (T item in this)
        {
            hash.Add(item);
        }

        return hash.ToHashCode();
    }

    /// <summary>Determines whether two <see cref="ValueList{T}"/> instances are equal by value.</summary>
    /// <param name="left">The first <see cref="ValueList{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueList{T}"/> to compare.</param>
    /// <returns><c>true</c> if the lists are equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ValueList<T>? left, ValueList<T>? right)
    {
        return Equals(left, right);
    }

    /// <summary>Determines whether two <see cref="ValueList{T}"/> instances are not equal by value.</summary>
    /// <param name="left">The first <see cref="ValueList{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueList{T}"/> to compare.</param>
    /// <returns><c>true</c> if the lists are not equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ValueList<T>? left, ValueList<T>? right)
    {
        return !Equals(left, right);
    }
}
