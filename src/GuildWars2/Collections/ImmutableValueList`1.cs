using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GuildWars2.Collections;

/// <summary>Represents an immutable list collection with value semantics, meaning two <see cref="ImmutableValueList{T}"/> instances are considered equal if their contents are equal.</summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
[CollectionBuilder(typeof(ImmutableValueList), nameof(ImmutableValueList.Create))]
[DebuggerDisplay("Count = {Count}")]
[SuppressMessage("Style", "IDE0028", Justification = "Cannot simplify constructor calls that wrap ImmutableList<T>.")]
[SuppressMessage("Style", "IDE0301", Justification = "Cannot simplify to collection expression.")]
[SuppressMessage("Style", "IDE0303", Justification = "Cannot simplify to collection expression.")]
public sealed class ImmutableValueList<T> : IImmutableValueList<T>
{
    /// <summary>Gets an empty <see cref="ImmutableValueList{T}"/>.</summary>
    [SuppressMessage("Design", "CA1000", Justification = "Follows BCL pattern for immutable collections.")]
    public static ImmutableValueList<T> Empty { get; } = new();

    /// <summary>Creates an <see cref="ImmutableValueList{T}"/> from a span of values. Used by collection expressions.</summary>
    /// <param name="values">The values to include in the list.</param>
    /// <returns>An <see cref="ImmutableValueList{T}"/> containing the specified values.</returns>
    [SuppressMessage("Design", "CA1000", Justification = "Required for CollectionBuilder attribute.")]
#pragma warning disable RCS1231 // CollectionBuilder requires exact signature without 'in'
    public static ImmutableValueList<T> Create(ReadOnlySpan<T> values)
#pragma warning restore RCS1231
    {
        if (values.IsEmpty)
        {
            return Empty;
        }

        T[] array = new T[values.Length];
        values.CopyTo(array);
#pragma warning disable IDE0306 // Cannot use collection expression in CollectionBuilder method
        return new ImmutableValueList<T>(array);
#pragma warning restore IDE0306
    }

    private readonly ImmutableList<T> items;

    /// <summary>Initializes a new instance of the <see cref="ImmutableValueList{T}"/> class that is empty.</summary>
    public ImmutableValueList()
    {
        items = ImmutableList<T>.Empty;
    }

    /// <summary>Initializes a new instance of the <see cref="ImmutableValueList{T}"/> class that contains elements copied from the specified collection.</summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public ImmutableValueList(IEnumerable<T> collection)
    {
        items = ImmutableList.CreateRange(collection);
    }

    private ImmutableValueList(ImmutableList<T> items)
    {
        this.items = items;
    }

    /// <inheritdoc/>
    public int Count => items.Count;

    /// <inheritdoc/>
    public T this[int index] => items[index];

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the specified item added to the end.</summary>
    /// <param name="value">The item to add.</param>
    /// <returns>A new list with the item added.</returns>
    public ImmutableValueList<T> Add(T value)
    {
        return new ImmutableValueList<T>(items.Add(value));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the specified items added to the end.</summary>
    /// <param name="items">The items to add.</param>
    /// <returns>A new list with the items added.</returns>
    public ImmutableValueList<T> AddRange(IEnumerable<T> items)
    {
        return new ImmutableValueList<T>(this.items.AddRange(items));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the specified item inserted at the specified index.</summary>
    /// <param name="index">The zero-based index at which to insert the item.</param>
    /// <param name="element">The item to insert.</param>
    /// <returns>A new list with the item inserted.</returns>
    public ImmutableValueList<T> Insert(int index, T element)
    {
        return new ImmutableValueList<T>(items.Insert(index, element));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the item at the specified index removed.</summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <returns>A new list with the item removed.</returns>
    public ImmutableValueList<T> RemoveAt(int index)
    {
        return new ImmutableValueList<T>(items.RemoveAt(index));
    }

    /// <summary>Searches for the specified item and returns the zero-based index of the first occurrence within the range of elements that starts at the specified index and contains the specified number of elements.</summary>
    /// <param name="item">The item to locate in the list.</param>
    /// <param name="index">The zero-based starting index of the search.</param>
    /// <param name="count">The number of elements in the section to search.</param>
    /// <param name="equalityComparer">The equality comparer to use in the search, or <c>null</c> to use the default comparer.</param>
    /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="equalityComparer"/>, if found; otherwise, -1.</returns>
    public int IndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer)
    {
        return items.IndexOf(item, index, count, equalityComparer);
    }

    /// <summary>Searches for the specified item and returns the zero-based index of the last occurrence within the range of elements that contains the specified number of elements and ends at the specified index.</summary>
    /// <param name="item">The item to locate in the list.</param>
    /// <param name="index">The zero-based starting index of the backward search.</param>
    /// <param name="count">The number of elements in the section to search.</param>
    /// <param name="equalityComparer">The equality comparer to use in the search, or <c>null</c> to use the default comparer.</param>
    /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="equalityComparer"/>, if found; otherwise, -1.</returns>
    public int LastIndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer)
    {
        return items.LastIndexOf(item, index, count, equalityComparer);
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the specified items inserted at the specified index.</summary>
    /// <param name="index">The zero-based index at which the new items should be inserted.</param>
    /// <param name="items">The items to insert.</param>
    /// <returns>A new list with the items inserted.</returns>
    public ImmutableValueList<T> InsertRange(int index, IEnumerable<T> items)
    {
        return new ImmutableValueList<T>(this.items.InsertRange(index, items));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the first occurrence of the specified item removed.</summary>
    /// <param name="value">The item to remove.</param>
    /// <param name="equalityComparer">The equality comparer to use to locate <paramref name="value"/>, or <c>null</c> to use the default comparer.</param>
    /// <returns>A new list with the item removed, or the same list if the item was not found.</returns>
    public ImmutableValueList<T> Remove(T value, IEqualityComparer<T>? equalityComparer)
    {
        return new ImmutableValueList<T>(items.Remove(value, equalityComparer));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with all elements that match the conditions defined by the specified predicate removed.</summary>
    /// <param name="match">The predicate that defines the conditions of the elements to remove.</param>
    /// <returns>A new list with the matching elements removed.</returns>
    public ImmutableValueList<T> RemoveAll(Predicate<T> match)
    {
        return new ImmutableValueList<T>(items.RemoveAll(match));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the specified items removed.</summary>
    /// <param name="items">The items to remove from the list.</param>
    /// <param name="equalityComparer">The equality comparer to use to locate the items, or <c>null</c> to use the default comparer.</param>
    /// <returns>A new list with the items removed.</returns>
    public ImmutableValueList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer)
    {
        return new ImmutableValueList<T>(this.items.RemoveRange(items, equalityComparer));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with a range of elements removed.</summary>
    /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
    /// <param name="count">The number of elements to remove.</param>
    /// <returns>A new list with the elements removed.</returns>
    public ImmutableValueList<T> RemoveRange(int index, int count)
    {
        return new ImmutableValueList<T>(items.RemoveRange(index, count));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the first occurrence of the specified value replaced with a new value.</summary>
    /// <param name="oldValue">The value to be replaced.</param>
    /// <param name="newValue">The value to replace the first occurrence of <paramref name="oldValue"/>.</param>
    /// <param name="equalityComparer">The equality comparer to use to locate <paramref name="oldValue"/>, or <c>null</c> to use the default comparer.</param>
    /// <returns>A new list with the value replaced.</returns>
    public ImmutableValueList<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer)
    {
        return new ImmutableValueList<T>(items.Replace(oldValue, newValue, equalityComparer));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with the element at the specified index replaced.</summary>
    /// <param name="index">The zero-based index of the element to replace.</param>
    /// <param name="value">The new value.</param>
    /// <returns>A new list with the element replaced.</returns>
    public ImmutableValueList<T> SetItem(int index, T value)
    {
        return new ImmutableValueList<T>(items.SetItem(index, value));
    }

    /// <summary>Creates a new <see cref="ImmutableValueList{T}"/> with all items removed.</summary>
    /// <returns>An empty list.</returns>
    public ImmutableValueList<T> Clear()
    {
        return Empty;
    }

    /// <summary>Determines whether the current <see cref="ImmutableValueList{T}"/> is equal to another <see cref="IImmutableValueList{T}"/> based on value semantics.</summary>
    /// <param name="other">The other <see cref="IImmutableValueList{T}"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the lists are equal by value; otherwise, <c>false</c>.</returns>
    public bool Equals(IImmutableValueList<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Count == other.Count && items.SequenceEqual(other);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is IImmutableValueList<T> other && Equals(other));
    }

    /// <summary>Returns a hash code based on the values of the items in the list.</summary>
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
        return items.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Determines whether two <see cref="ImmutableValueList{T}"/> instances are equal by value.</summary>
    /// <param name="left">The first <see cref="ImmutableValueList{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ImmutableValueList{T}"/> to compare.</param>
    /// <returns><c>true</c> if the lists are equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ImmutableValueList<T>? left, ImmutableValueList<T>? right)
    {
        return Equals(left, right);
    }

    /// <summary>Determines whether two <see cref="ImmutableValueList{T}"/> instances are not equal by value.</summary>
    /// <param name="left">The first <see cref="ImmutableValueList{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="ImmutableValueList{T}"/> to compare.</param>
    /// <returns><c>true</c> if the lists are not equal by value; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ImmutableValueList<T>? left, ImmutableValueList<T>? right)
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

    #region Explicit IImmutableValueList<T> implementation

    IImmutableValueList<T> IImmutableValueList<T>.Add(T value)
    {
        return Add(value);
    }

    IImmutableValueList<T> IImmutableValueList<T>.AddRange(IEnumerable<T> items)
    {
        return AddRange(items);
    }

    IImmutableValueList<T> IImmutableValueList<T>.Clear()
    {
        return Clear();
    }

    IImmutableValueList<T> IImmutableValueList<T>.Insert(int index, T element)
    {
        return Insert(index, element);
    }

    IImmutableValueList<T> IImmutableValueList<T>.InsertRange(int index, IEnumerable<T> items)
    {
        return InsertRange(index, items);
    }

    IImmutableValueList<T> IImmutableValueList<T>.Remove(T value, IEqualityComparer<T>? equalityComparer)
    {
        return Remove(value, equalityComparer);
    }

    IImmutableValueList<T> IImmutableValueList<T>.RemoveAll(Predicate<T> match)
    {
        return RemoveAll(match);
    }

    IImmutableValueList<T> IImmutableValueList<T>.RemoveAt(int index)
    {
        return RemoveAt(index);
    }

    IImmutableValueList<T> IImmutableValueList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer)
    {
        return RemoveRange(items, equalityComparer);
    }

    IImmutableValueList<T> IImmutableValueList<T>.RemoveRange(int index, int count)
    {
        return RemoveRange(index, count);
    }

    IImmutableValueList<T> IImmutableValueList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer)
    {
        return Replace(oldValue, newValue, equalityComparer);
    }

    IImmutableValueList<T> IImmutableValueList<T>.SetItem(int index, T value)
    {
        return SetItem(index, value);
    }

    #endregion Explicit IImmutableValueList<T> implementation
}
