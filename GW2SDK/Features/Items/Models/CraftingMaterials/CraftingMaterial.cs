namespace GuildWars2.Items;

/// <summary>Information about a crafting material.</summary>
[PublicAPI]
public sealed record CraftingMaterial : Item
{
    /// <summary>If the current material is used to infuse equipment, this collection contains the IDs of the infused items.
    /// Each item in the collection represents a possible upgrade path.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }
}
