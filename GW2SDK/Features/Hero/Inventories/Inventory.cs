namespace GuildWars2.Hero.Inventories;

/// <summary>Information about items in a bag or shared inventory.</summary>
[PublicAPI]
public sealed record Inventory
{
    /// <summary>The item slots in the bag. Empty slots are represented as <c>null</c>.</summary>
    public required IReadOnlyList<ItemSlot?> Items { get; init; }
}
