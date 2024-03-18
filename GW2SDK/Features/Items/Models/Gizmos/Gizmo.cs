namespace GuildWars2.Items;

/// <summary>Information about a gizmo, which is a type of item that is similar to a <see cref="Consumable" />, but it is
/// typically not consumed after one use. Some gizmo items are donated to your current guild when used, such as guild
/// decoration items. Gizmos are also used for items that provide a bundle, such as Catapult Blueprints, in which case
/// using a bundle skill might consume the item. Some Gizmos, like Mystic Forge Stones, have no use on their own. They are
/// combined with other items to create a new item, which destroys the original item.</summary>
[PublicAPI]
[Inheritable]
public record Gizmo : Item
{
    /// <summary>If this item can be donated to a guild, this is the ID of the associated guild upgrade.</summary>
    public required int? GuildUpgradeId { get; init; }
}
