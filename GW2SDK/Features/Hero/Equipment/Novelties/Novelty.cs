namespace GuildWars2.Hero.Equipment.Novelties;

[PublicAPI]
[DataTransferObject]
public sealed record Novelty
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    /// <summary>The URL of the novelty icon.</summary>
    public required string IconHref { get; init; }

    public required NoveltyKind Slot { get; init; }

    /// <summary>The IDs of the items that unlock the novelty when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }
}
