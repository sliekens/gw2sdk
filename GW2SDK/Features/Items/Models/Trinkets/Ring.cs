namespace GuildWars2.Items;

/// <summary>Information about a ring.</summary>
[PublicAPI]
public sealed record Ring : Trinket
{
    /// <summary>If the current ring can be infused or attuned, this collection contains the IDs of the infused/attuned
    /// variations of the ring. Each item in the collection represents a possible upgrade path.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }

    /// <summary>If the current ring is infused or attuned, this collection contains the IDs of possible source items.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradeSource> UpgradesFrom { get; init; }
}
