namespace GuildWars2.Items;

/// <summary>Information about a gift box, which is used to represent most dye kits and some boss drops such as Drakkar's Hoard.</summary>
/// <remarks>It's unclear where the name GiftBox comes from.</remarks>
[PublicAPI]
public sealed record GiftBox : Container;
