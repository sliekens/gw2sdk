namespace GuildWars2.Pvp.MistChampions;

[PublicAPI]
[DataTransferObject]
public sealed record MistChampionSkin
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string IconHref { get; init; }

    public required bool Default { get; init; }

    /// <summary>The IDs of the items that unlock the mist champion skin when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }
}
