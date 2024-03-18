namespace GuildWars2.Items;

/// <summary>Information about a currency consumable.</summary>
/// <remarks>In the past, not every currency could be stored in the account wallet. This is no longer the case, and most
/// currencies have been converted to <see cref="Service" /> items. The few items of this type that remain also take effect
/// immediately on receipt.</remarks>
[PublicAPI]
public sealed record Currency : Consumable;
