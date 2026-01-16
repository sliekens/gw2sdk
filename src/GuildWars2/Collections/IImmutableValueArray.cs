namespace GuildWars2.Collections;

/// <summary>Represents an immutable array with value semantics for equality comparison.</summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
public interface IImmutableValueArray<T> : IImmutableList<T>, IEquatable<IImmutableValueArray<T>>;
