namespace GuildWars2.Collections;

/// <summary>Provides static methods for creating <see cref="ImmutableValueList{T}"/> instances.</summary>
public static class ImmutableValueList
{
#pragma warning disable RCS1231 // CollectionBuilder requires exact signature without 'in'
    /// <summary>Creates an <see cref="ImmutableValueList{T}"/> from a span of values. Used by collection expressions.</summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="values">The values to include in the list.</param>
    /// <returns>An <see cref="ImmutableValueList{T}"/> containing the specified values.</returns>
    public static ImmutableValueList<T> Create<T>(ReadOnlySpan<T> values)
    {
        return ImmutableValueList<T>.Create(values);
    }
}
#pragma warning restore RCS1231
