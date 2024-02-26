namespace GuildWars2;

/// <summary>Represents the context of an entity set.</summary>
/// <param name="ResultTotal">The total number of results available.</param>
/// <param name="ResultCount">The number of results in the current context.</param>
[PublicAPI]
public sealed record ResultContext(int ResultTotal, int ResultCount);
