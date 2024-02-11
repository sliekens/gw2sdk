namespace GuildWars2.Hero.Equipment.Novelties;

/// <summary>Information about a novelty.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Novelty
{
    /// <summary>The novelty ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the novelty.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the novelty.</summary>
    public required string Description { get; init; }

    /// <summary>The URL of the novelty icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The slot occupied by the novelty.</summary>
    public required NoveltyKind Slot { get; init; }

    /// <summary>The IDs of the items that unlock the novelty when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }
}
