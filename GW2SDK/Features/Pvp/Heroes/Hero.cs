namespace GuildWars2.Pvp.Heroes;

[PublicAPI]
[DataTransferObject]
public sealed record Hero
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Type { get; init; }

    public required HeroStats Stats { get; init; }

    public required string Overlay { get; init; }

    public required string Underlay { get; init; }

    public required IReadOnlyCollection<HeroSkin> Skins { get; init; }
}
