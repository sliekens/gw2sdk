namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record Division
{
    public required string Name { get; init; }

    public required DivisionFlags Flags { get; init; }

    /// <summary>The URL of the large division icon.</summary>
    public required string LargeIconHref { get; init; }

    /// <summary>The URL of the small division icon.</summary>
    public required string SmallIconHref { get; init; }

    /// <summary>The URL of the pip icon.</summary>
    public required string PipIconHref { get; init; }

    public required IReadOnlyList<DivisionTier> Tiers { get; init; }
}
