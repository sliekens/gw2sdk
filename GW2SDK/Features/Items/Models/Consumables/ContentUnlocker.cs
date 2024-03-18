namespace GuildWars2.Items;

/// <summary>Information about a consumable that unlocks content when consumed, (for example) a collection achievement,
/// mail carrier, finisher or commander tag.</summary>
[PublicAPI]
public sealed record ContentUnlocker : Unlocker;
