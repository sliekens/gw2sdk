namespace GuildWars2.Collections;

/// <summary>Represents an immutable list with value semantics for equality comparison.</summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public interface IImmutableValueList<T> : IImmutableList<T>, IEquatable<IImmutableValueList<T>>;
