namespace GuildWars2.Items;

/// <summary>Information about a crafting material.</summary>
[PublicAPI]
public sealed record CraftingMaterial : Item
{
    /// <summary>The IDs of other items that the current item can be upgraded into. Each item in the collection represents a
    /// possible upgrade path.</summary>
    public required IReadOnlyCollection<ItemUpgrade> UpgradesInto { get; init; }
}
