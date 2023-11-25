namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record Division
{
    public required string Name { get; init; }

    public required IReadOnlyCollection<DivisionFlag> Flags { get; init; }

    public required string LargeIconHref { get; init; }

    public required string SmallIconHref { get; init; }

    public required string PipIconHref { get; init; }

    public required IReadOnlyCollection<DivisionTier> Tiers { get; init; }
}
