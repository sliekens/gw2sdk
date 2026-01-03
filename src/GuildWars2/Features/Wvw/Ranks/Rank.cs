namespace GuildWars2.Wvw.Ranks;

/// <summary>Information about a World Rank.</summary>
[DataTransferObject]
public sealed record Rank
{
    /// <summary>The rank ID.</summary>
    public required int Id { get; init; }

    /// <summary>The rank title.</summary>
    public required string Title { get; init; }

    /// <summary>The number of World Ranks required to unlock this title.</summary>
    public required int MinRank { get; init; }
}
