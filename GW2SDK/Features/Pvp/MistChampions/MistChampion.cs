namespace GuildWars2.Pvp.MistChampions;

[PublicAPI]
[DataTransferObject]
public sealed record MistChampion
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Type { get; init; }

    public required MistChampionStats Stats { get; init; }

    public required string OverlayImageHref { get; init; }

    public required string UnderlayImageHref { get; init; }

    public required IReadOnlyCollection<MistChampionSkin> Skins { get; init; }
}
