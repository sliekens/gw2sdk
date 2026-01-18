namespace GuildWars2.Collections;

/// <summary>Provides static methods for creating <see cref="ImmutableValueArray{T}"/> instances.</summary>
public static class ImmutableValueArray
{
#pragma warning disable RCS1231 // CollectionBuilder requires exact signature without 'in'
    /// <summary>Creates an <see cref="ImmutableValueArray{T}"/> from a span of values. Used by collection expressions.</summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="values">The values to include in the array.</param>
    /// <returns>An <see cref="ImmutableValueArray{T}"/> containing the specified values.</returns>
    public static ImmutableValueArray<T> Create<T>(ReadOnlySpan<T> values)
    {
        return ImmutableValueArray<T>.Create(values);
    }
}
#pragma warning restore RCS1231
