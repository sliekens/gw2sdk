using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace GuildWars2.Collections;

/// <summary>Represents an immutable array with value semantics, meaning two <see cref="ValueImmutableArray{T}"/> instances are considered equal if their contents are equal.</summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
/// <remarks>This type wraps <see cref="ImmutableArray{T}"/> and is more memory-efficient than <see cref="ValueImmutableList{T}"/> for fixed-size collections.</remarks>
[DebuggerDisplay("Length = {Length}")]
[SuppressMessage("Style", "IDE0028", Justification = "Cannot simplify constructor calls that wrap ImmutableArray<T>.")]
[SuppressMessage("Style", "IDE0301", Justification = "Cannot simplify to collection expression.")]
[SuppressMessage("Style", "IDE0303", Justification = "Cannot simplify to collection expression.")]
public sealed class ValueImmutableArray<T> : IImmutableList<T>, IEquatable<ValueImmutableArray<T>>
{
    /// <summary>Gets an empty <see cref="ValueImmutableArray{T}"/>.</summary>
    [SuppressMessage("Design", "CA1000", Justification = "Follows BCL pattern for immutable collections.")]
    public static ValueImmutableArray<T> Empty { get; } = new();

    private readonly ImmutableArray<T> items;

    /// <summary>Initializes a new instance of the <see cref="ValueImmutableArray{T}"/> class that is empty.</summary>
    public ValueImmutableArray()
    {
        items = ImmutableArray<T>.Empty;
    }

    /// <summary>Initializes a new instance of the <see cref="ValueImmutableArray{T}"/> class that contains elements copied from the specified collection.</summary>
    /// <param name="collection">The collection whose elements are copied to the new array.</param>
    public ValueImmutableArray(IEnumerable<T> collection)
    {
        items = ImmutableArray.CreateRange(collection);
    }

#if NET
    /// <summary>Initializes a new instance of the <see cref="ValueImmutableArray{T}"/> class that contains elements copied from the specified span.</summary>
    /// <param name="items">The span whose elements are copied to the new array.</param>
    public ValueImmutableArray(in ReadOnlySpan<T> items)
    {
        this.items = [.. items];
    }
#endif

    private ValueImmutableArray(in ImmutableArray<T> items)
    {
        this.items = items;
    }

    /// <summary>Gets the number of elements in the array.</summary>
    public int Length => items.Length;

    /// <inheritdoc/>
    public int Count => items.Length;

    /// <inheritdoc/>
    public T this[int index] => items[index];

#if NET
    /// <summary>Gets the underlying <see cref="ImmutableArray{T}"/> as a <see cref="ReadOnlySpan{T}"/>.</summary>
    /// <returns>A read-only span of the array elements.</returns>
    public ReadOnlySpan<T> AsSpan()
    {
        return items.AsSpan();
    }

    /// <summary>Gets the underlying <see cref="ImmutableArray{T}"/> as a <see cref="ReadOnlyMemory{T}"/>.</summary>
    /// <returns>A read-only memory of the array elements.</returns>
    public ReadOnlyMemory<T> AsMemory()
    {
        return items.AsMemory();
    }
#endif

    /// <summary>Determines whether the array contains the specified item.</summary>
    /// <param name="item">The item to locate.</param>
    /// <returns><c>true</c> if the item is found; otherwise, <c>false</c>.</returns>
    public bool Contains(T item)
    {
        return items.Contains(item);
    }

    /// <inheritdoc/>
    public int IndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer)
    {
        return items.IndexOf(item, index, count, equalityComparer);
    }

    /// <inheritdoc/>
    public int LastIndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer)
    {
        return items.LastIndexOf(item, index, count, equalityComparer);
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the specified item added to the end.</summary>
    /// <param name="value">The item to add.</param>
    /// <returns>A new array with the item added.</returns>
    public ValueImmutableArray<T> Add(T value)
    {
        return new ValueImmutableArray<T>(items.Add(value));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the specified items added to the end.</summary>
    /// <param name="values">The items to add.</param>
    /// <returns>A new array with the items added.</returns>
    public ValueImmutableArray<T> AddRange(IEnumerable<T> values)
    {
        return new ValueImmutableArray<T>(items.AddRange(values));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the specified item inserted at the specified index.</summary>
    /// <param name="index">The zero-based index at which to insert the item.</param>
    /// <param name="element">The item to insert.</param>
    /// <returns>A new array with the item inserted.</returns>
    public ValueImmutableArray<T> Insert(int index, T element)
    {
        return new ValueImmutableArray<T>(items.Insert(index, element));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the specified items inserted at the specified index.</summary>
    /// <param name="index">The zero-based index at which to insert the items.</param>
    /// <param name="elements">The items to insert.</param>
    /// <returns>A new array with the items inserted.</returns>
    public ValueImmutableArray<T> InsertRange(int index, IEnumerable<T> elements)
    {
        return new ValueImmutableArray<T>(items.InsertRange(index, elements));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the item at the specified index removed.</summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <returns>A new array with the item removed.</returns>
    public ValueImmutableArray<T> RemoveAt(int index)
    {
        return new ValueImmutableArray<T>(items.RemoveAt(index));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the first occurrence of the specified item removed.</summary>
    /// <param name="value">The item to remove.</param>
    /// <param name="equalityComparer">The equality comparer to use.</param>
    /// <returns>A new array with the item removed, or the same array if the item was not found.</returns>
    public ValueImmutableArray<T> Remove(T value, IEqualityComparer<T>? equalityComparer)
    {
        ImmutableArray<T> newItems = items.Remove(value, equalityComparer);
        return newItems.Length == items.Length ? this : new ValueImmutableArray<T>(newItems);
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with all items that match the specified predicate removed.</summary>
    /// <param name="match">The predicate to match items to remove.</param>
    /// <returns>A new array with the matching items removed.</returns>
    public ValueImmutableArray<T> RemoveAll(Predicate<T> match)
    {
        return new ValueImmutableArray<T>(items.RemoveAll(match));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the specified items removed.</summary>
    /// <param name="elementsToRemove">The items to remove.</param>
    /// <param name="equalityComparer">The equality comparer to use.</param>
    /// <returns>A new array with the items removed.</returns>
    public ValueImmutableArray<T> RemoveRange(IEnumerable<T> elementsToRemove, IEqualityComparer<T>? equalityComparer)
    {
        return new ValueImmutableArray<T>(items.RemoveRange(elementsToRemove, equalityComparer));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with a range of items removed.</summary>
    /// <param name="index">The zero-based starting index of the range to remove.</param>
    /// <param name="count">The number of items to remove.</param>
    /// <returns>A new array with the range removed.</returns>
    public ValueImmutableArray<T> RemoveRange(int index, int count)
    {
        return new ValueImmutableArray<T>(items.RemoveRange(index, count));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the first occurrence of the old value replaced with the new value.</summary>
    /// <param name="oldValue">The value to replace.</param>
    /// <param name="newValue">The replacement value.</param>
    /// <param name="equalityComparer">The equality comparer to use.</param>
    /// <returns>A new array with the replacement made.</returns>
    public ValueImmutableArray<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer)
    {
        return new ValueImmutableArray<T>(items.Replace(oldValue, newValue, equalityComparer));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with the element at the specified index replaced.</summary>
    /// <param name="index">The zero-based index of the element to replace.</param>
    /// <param name="value">The new value.</param>
    /// <returns>A new array with the element replaced.</returns>
    public ValueImmutableArray<T> SetItem(int index, T value)
    {
        return new ValueImmutableArray<T>(items.SetItem(index, value));
    }

    /// <summary>Creates a new <see cref="ValueImmutableArray{T}"/> with all items removed.</summary>
    /// <returns>An empty array.</returns>
    public ValueImmutableArray<T> Clear()
    {
        return Empty;
    }

    /// <summary>Determines whether the current <see cref="ValueImmutableArray{T}"/> is equal to another <see cref="ValueImmutableArray{T}"/> based on value semantics.</summary>
    /// <param name="other">The other <see cref="ValueImmutableArray{T}"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the arrays are equal by value; otherwise, <c>false</c>.</returns>
    public bool Equals(ValueImmutableArray<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return items.SequenceEqual(other.items);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is ValueImmutableArray<T> other && Equals(other));
    }

    /// <summary>Returns a hash code based on the values of the items in the array.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        HashCode hash = new();
        foreach (T item in items)
        {
            hash.Add(item);
        }

        return hash.ToHashCode();
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)items).GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Determines whether two <see cref="ValueImmutableArray{T}"/> instances are equal by value.</summary>
    /// <param name="left">The first <see cref="ValueImmutableArray{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueImmutableArray{T}"/> to compare.</param>
    /// <returns><c>true</c> if the arrays are equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ValueImmutableArray<T>? left, ValueImmutableArray<T>? right)
    {
        return Equals(left, right);
    }

    /// <summary>Determines whether two <see cref="ValueImmutableArray{T}"/> instances are not equal by value.</summary>
    /// <param name="left">The first <see cref="ValueImmutableArray{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ValueImmutableArray{T}"/> to compare.</param>
    /// <returns><c>true</c> if the arrays are not equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ValueImmutableArray<T>? left, ValueImmutableArray<T>? right)
    {
        return !Equals(left, right);
    }

    #region Explicit IImmutableList<T> implementation

    IImmutableList<T> IImmutableList<T>.Add(T value)
    {
        return Add(value);
    }

    IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items)
    {
        return AddRange(items);
    }

    IImmutableList<T> IImmutableList<T>.Clear()
    {
        return Clear();
    }

    IImmutableList<T> IImmutableList<T>.Insert(int index, T element)
    {
        return Insert(index, element);
    }

    IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items)
    {
        return InsertRange(index, items);
    }

    IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T>? equalityComparer)
    {
        return Remove(value, equalityComparer);
    }

    IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match)
    {
        return RemoveAll(match);
    }

    IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
    {
        return RemoveAt(index);
    }

    IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer)
    {
        return RemoveRange(items, equalityComparer);
    }

    IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count)
    {
        return RemoveRange(index, count);
    }

    IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer)
    {
        return Replace(oldValue, newValue, equalityComparer);
    }

    IImmutableList<T> IImmutableList<T>.SetItem(int index, T value)
    {
        return SetItem(index, value);
    }

    #endregion Explicit IImmutableList<T> implementation
}
