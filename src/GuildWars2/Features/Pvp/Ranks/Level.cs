namespace GuildWars2.Pvp.Ranks;

/// <summary>Information about the rank points required to gain a rank level.</summary>
[DataTransferObject]
public sealed record Level
{
    /// <summary>Starting at this rank level, it takes the number of rank points indicated by <see cref="Points" /> to gain a
    /// rank level.</summary>
    public required int MinRank { get; init; }

    /// <summary>Up until this rank level, it takes the number of rank points indicated by <see cref="Points" /> to gain a rank
    /// level.</summary>
    public required int MaxRank { get; init; }

    /// <summary>The number of rank points required to reach the next rank level.</summary>
    public required int Points { get; init; }
}
