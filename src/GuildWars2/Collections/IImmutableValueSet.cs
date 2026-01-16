namespace GuildWars2.Collections;

/// <summary>Represents an immutable set with value semantics for equality comparison.</summary>
/// <typeparam name="T">The type of elements in the set.</typeparam>
public interface IImmutableValueSet<T> : IImmutableSet<T>, IEquatable<IImmutableValueSet<T>>;
