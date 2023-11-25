namespace GuildWars2.Pvp.MistChampions;

[PublicAPI]
[DataTransferObject]
public sealed record MistChampionSkin
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string IconHref { get; init; }

    public required bool Default { get; init; }

    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }
}
