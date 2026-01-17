using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GuildWars2.Collections;

/// <summary>Represents an immutable set collection with value semantics, meaning two <see cref="ImmutableValueSet{T}"/> instances are considered equal if their contents are equal.</summary>
/// <typeparam name="T">The type of elements in the set.</typeparam>
[CollectionBuilder(typeof(ImmutableValueSet), nameof(ImmutableValueSet.Create))]
[DebuggerDisplay("Count = {Count}")]
[SuppressMessage("Style", "IDE0028", Justification = "Cannot simplify constructor calls that wrap ImmutableHashSet<T>.")]
[SuppressMessage("Style", "IDE0301", Justification = "Cannot simplify to collection expression.")]
[SuppressMessage("Style", "IDE0303", Justification = "Cannot simplify to collection expression.")]
public sealed class ImmutableValueSet<T> : IImmutableValueSet<T>
{
    /// <summary>Gets an empty <see cref="ImmutableValueSet{T}"/>.</summary>
    [SuppressMessage("Design", "CA1000", Justification = "Follows BCL pattern for immutable collections.")]
    public static ImmutableValueSet<T> Empty { get; } = new();

    /// <summary>Creates an <see cref="ImmutableValueSet{T}"/> from a span of values. Used by collection expressions.</summary>
    /// <param name="values">The values to include in the set.</param>
    /// <returns>An <see cref="ImmutableValueSet{T}"/> containing the specified values.</returns>
    [SuppressMessage("Design", "CA1000", Justification = "Required for CollectionBuilder attribute.")]
#pragma warning disable RCS1231 // CollectionBuilder requires exact signature without 'in'
    public static ImmutableValueSet<T> Create(ReadOnlySpan<T> values)
#pragma warning restore RCS1231
    {
        if (values.IsEmpty)
        {
            return Empty;
        }

        T[] array = new T[values.Length];
        values.CopyTo(array);
#pragma warning disable IDE0306 // Cannot use collection expression in CollectionBuilder method
        return new ImmutableValueSet<T>(array);
#pragma warning restore IDE0306
    }

    private readonly ImmutableHashSet<T> items;

    /// <summary>Initializes a new instance of the <see cref="ImmutableValueSet{T}"/> class that is empty.</summary>
    public ImmutableValueSet()
    {
        items = ImmutableHashSet<T>.Empty;
    }

    /// <summary>Initializes a new instance of the <see cref="ImmutableValueSet{T}"/> class that contains elements copied from the specified collection.</summary>
    /// <param name="collection">The collection whose elements are copied to the new set.</param>
    public ImmutableValueSet(IEnumerable<T> collection)
    {
        items = ImmutableHashSet.CreateRange(collection);
    }

    private ImmutableValueSet(ImmutableHashSet<T> items)
    {
        this.items = items;
    }

    /// <inheritdoc/>
    public int Count => items.Count;

    /// <summary>Determines whether the set contains the specified value.</summary>
    /// <param name="value">The value to locate in the set.</param>
    /// <returns><c>true</c> if the set contains the specified value; otherwise, <c>false</c>.</returns>
    public bool Contains(T value)
    {
        return items.Contains(value);
    }

    /// <summary>Creates a new <see cref="ImmutableValueSet{T}"/> with the specified item added.</summary>
    /// <param name="value">The item to add.</param>
    /// <returns>A new set with the item added, or the same set if the item already exists.</returns>
    public ImmutableValueSet<T> Add(T value)
    {
        ImmutableHashSet<T> newItems = items.Add(value);
        return ReferenceEquals(newItems, items) ? this : new ImmutableValueSet<T>(newItems);
    }

    /// <summary>Creates a new <see cref="ImmutableValueSet{T}"/> with the specified item removed.</summary>
    /// <param name="value">The item to remove.</param>
    /// <returns>A new set with the item removed, or the same set if the item was not found.</returns>
    public ImmutableValueSet<T> Remove(T value)
    {
        ImmutableHashSet<T> newItems = items.Remove(value);
        return ReferenceEquals(newItems, items) ? this : new ImmutableValueSet<T>(newItems);
    }

    /// <summary>Creates a new <see cref="ImmutableValueSet{T}"/> with all items removed.</summary>
    /// <returns>An empty set.</returns>
    public ImmutableValueSet<T> Clear()
    {
        return Empty;
    }

    /// <summary>Creates a new <see cref="ImmutableValueSet{T}"/> that is the union of this set and the specified collection.</summary>
    /// <param name="other">The collection to union with.</param>
    /// <returns>A new set containing all elements from both this set and the specified collection.</returns>
    public ImmutableValueSet<T> Union(IEnumerable<T> other)
    {
        return new ImmutableValueSet<T>(items.Union(other));
    }

    /// <summary>Creates a new <see cref="ImmutableValueSet{T}"/> that is the intersection of this set and the specified collection.</summary>
    /// <param name="other">The collection to intersect with.</param>
    /// <returns>A new set containing only elements present in both this set and the specified collection.</returns>
    public ImmutableValueSet<T> Intersect(IEnumerable<T> other)
    {
        return new ImmutableValueSet<T>(items.Intersect(other));
    }

    /// <summary>Creates a new <see cref="ImmutableValueSet{T}"/> that contains elements in this set but not in the specified collection.</summary>
    /// <param name="other">The collection to except.</param>
    /// <returns>A new set containing elements present in this set but not in the specified collection.</returns>
    public ImmutableValueSet<T> Except(IEnumerable<T> other)
    {
        return new ImmutableValueSet<T>(items.Except(other));
    }

    /// <summary>Determines whether this set is a subset of the specified collection.</summary>
    /// <param name="other">The collection to compare.</param>
    /// <returns><c>true</c> if this set is a subset of the specified collection; otherwise, <c>false</c>.</returns>
    public bool IsSubsetOf(IEnumerable<T> other)
    {
        return items.IsSubsetOf(other);
    }

    /// <summary>Determines whether this set is a superset of the specified collection.</summary>
    /// <param name="other">The collection to compare.</param>
    /// <returns><c>true</c> if this set is a superset of the specified collection; otherwise, <c>false</c>.</returns>
    public bool IsSupersetOf(IEnumerable<T> other)
    {
        return items.IsSupersetOf(other);
    }

    /// <summary>Determines whether this set and the specified collection share any common elements.</summary>
    /// <param name="other">The collection to compare.</param>
    /// <returns><c>true</c> if this set and the specified collection share at least one common element; otherwise, <c>false</c>.</returns>
    public bool Overlaps(IEnumerable<T> other)
    {
        return items.Overlaps(other);
    }

    /// <summary>Determines whether this set and the specified collection contain the same elements.</summary>
    /// <param name="other">The collection to compare.</param>
    /// <returns><c>true</c> if this set and the specified collection contain the same elements; otherwise, <c>false</c>.</returns>
    public bool SetEquals(IEnumerable<T> other)
    {
        return items.SetEquals(other);
    }

    /// <summary>Determines whether this set is a proper subset of the specified collection.</summary>
    /// <param name="other">The collection to compare.</param>
    /// <returns><c>true</c> if this set is a proper subset of the specified collection; otherwise, <c>false</c>.</returns>
    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
        return items.IsProperSubsetOf(other);
    }

    /// <summary>Determines whether this set is a proper superset of the specified collection.</summary>
    /// <param name="other">The collection to compare.</param>
    /// <returns><c>true</c> if this set is a proper superset of the specified collection; otherwise, <c>false</c>.</returns>
    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
        return items.IsProperSupersetOf(other);
    }

    /// <summary>Creates a new <see cref="ImmutableValueSet{T}"/> that contains elements present in either this set or the specified collection, but not both.</summary>
    /// <param name="other">The collection to compare.</param>
    /// <returns>A new set containing elements present in either this set or the specified collection, but not both.</returns>
    public ImmutableValueSet<T> SymmetricExcept(IEnumerable<T> other)
    {
        return new ImmutableValueSet<T>(items.SymmetricExcept(other));
    }

    /// <summary>Searches the set for a given value and returns the equal value it finds, if any.</summary>
    /// <param name="equalValue">The value to search for.</param>
    /// <param name="actualValue">The value from the set that the search found, or the original value if the search yielded no match.</param>
    /// <returns><c>true</c> if the search was successful; otherwise, <c>false</c>.</returns>
    public bool TryGetValue(T equalValue, out T actualValue)
    {
        return items.TryGetValue(equalValue, out actualValue!);
    }

    /// <summary>Determines whether the current <see cref="ImmutableValueSet{T}"/> is equal to another <see cref="IImmutableValueSet{T}"/> based on value semantics.</summary>
    /// <param name="other">The other <see cref="IImmutableValueSet{T}"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the sets are equal by value; otherwise, <c>false</c>.</returns>
    public bool Equals(IImmutableValueSet<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return items.SetEquals(other);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is IImmutableValueSet<T> other && Equals(other));
    }

    /// <summary>Returns a hash code based on the values of the items in the set.</summary>
    /// <returns>A hash code for the current object.</returns>
    /// <remarks>Uses XOR to combine hash codes so that order does not affect the result.</remarks>
    public override int GetHashCode()
    {
        int hash = 0;
        foreach (T item in items)
        {
            hash ^= item?.GetHashCode() ?? 0;
        }

        return hash;
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Determines whether two <see cref="ImmutableValueSet{T}"/> instances are equal by value.</summary>
    /// <param name="left">The first <see cref="ImmutableValueSet{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ImmutableValueSet{T}"/> to compare.</param>
    /// <returns><c>true</c> if the sets are equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ImmutableValueSet<T>? left, ImmutableValueSet<T>? right)
    {
        return Equals(left, right);
    }

    /// <summary>Determines whether two <see cref="ImmutableValueSet{T}"/> instances are not equal by value.</summary>
    /// <param name="left">The first <see cref="ImmutableValueSet{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ImmutableValueSet{T}"/> to compare.</param>
    /// <returns><c>true</c> if the sets are not equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ImmutableValueSet<T>? left, ImmutableValueSet<T>? right)
    {
        return !Equals(left, right);
    }

    #region Explicit IImmutableSet<T> implementation

    IImmutableSet<T> IImmutableSet<T>.Add(T value)
    {
        return Add(value);
    }

    IImmutableSet<T> IImmutableSet<T>.Clear()
    {
        return Clear();
    }

    IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other)
    {
        return Except(other);
    }

    IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other)
    {
        return Intersect(other);
    }

    IImmutableSet<T> IImmutableSet<T>.Remove(T value)
    {
        return Remove(value);
    }

    IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other)
    {
        return SymmetricExcept(other);
    }

    IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other)
    {
        return Union(other);
    }

    #endregion Explicit IImmutableSet<T> implementation
}
