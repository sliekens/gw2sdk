namespace GuildWars2.Items;

/// <summary>Information about a bag.</summary>
[PublicAPI]
public sealed record Bag : Item
{
    /// <summary>Whether the bag is invisible/safe, meaning items in the bag appear do not appear in sell-to-vendor lists or
    /// salvage lists and will not be affected by "Deposit Collectibles".</summary>
    public required bool NoSellOrSort { get; init; }

    /// <summary>The capacity of the bag.</summary>
    public required int Size { get; init; }
}
