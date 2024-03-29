namespace GuildWars2.Pvp.Ranks;

/// <summary>Information about a PvP rank.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Rank
{
    /// <summary>The rank ID.</summary>
    public required int Id { get; init; }

    /// <summary>The ID of the finisher associated with the rank.</summary>
    public required int FinisherId { get; init; }

    /// <summary>The rank name.</summary>
    public required string Name { get; init; }

    /// <summary>The URL of the rank icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The current rank starts at this rank level.</summary>
    public required int MinRank { get; init; }

    /// <summary>The current rank goes up until this rank level.</summary>
    public required int MaxRank { get; init; }

    /// <summary>The rank levels and the rank points required to gain a level within this rank.</summary>
    public required IReadOnlyCollection<Level> Levels { get; init; }
}
