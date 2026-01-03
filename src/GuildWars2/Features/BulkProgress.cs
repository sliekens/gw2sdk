namespace GuildWars2;

/// <summary>Represents the progress of a bulk query.</summary>
/// <param name="ResultTotal">The total number of results on the server.</param>
/// <param name="ResultCount">The number of results already retrieved.</param>
public sealed record BulkProgress(int ResultTotal, int ResultCount);
