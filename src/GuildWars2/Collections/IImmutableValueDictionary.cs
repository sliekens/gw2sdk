namespace GuildWars2.Collections;

/// <summary>Represents an immutable dictionary with value semantics for equality comparison.</summary>
/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
public interface IImmutableValueDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>, IEquatable<IImmutableValueDictionary<TKey, TValue>>
    where TKey : notnull;
