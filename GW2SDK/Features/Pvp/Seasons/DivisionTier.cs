namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a tier within a division.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record DivisionTier
{
    /// <summary>The number of pips required to complete the tier.</summary>
    public required int Points { get; init; }
}
