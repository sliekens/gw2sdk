namespace GuildWars2.Inventories;

/// <summary>Information about items in a bag or shared inventory.</summary>
[PublicAPI]
public sealed record Inventory
{
    public required IReadOnlyList<ItemSlot?> Items { get; init; }
}
