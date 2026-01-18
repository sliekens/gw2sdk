namespace GuildWars2.Collections;

/// <summary>Provides static methods for creating <see cref="ImmutableValueSet{T}"/> instances.</summary>
public static class ImmutableValueSet
{
#pragma warning disable RCS1231 // CollectionBuilder requires exact signature without 'in'
    /// <summary>Creates an <see cref="ImmutableValueSet{T}"/> from a span of values. Used by collection expressions.</summary>
    /// <typeparam name="T">The type of elements in the set.</typeparam>
    /// <param name="values">The values to include in the set.</param>
    /// <returns>An <see cref="ImmutableValueSet{T}"/> containing the specified values.</returns>
    public static ImmutableValueSet<T> Create<T>(ReadOnlySpan<T> values)
    {
        return ImmutableValueSet<T>.Create(values);
    }
}
#pragma warning restore RCS1231
