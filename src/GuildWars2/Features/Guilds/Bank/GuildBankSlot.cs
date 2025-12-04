namespace GuildWars2.Guilds.Bank;

/// <summary>Information about an item in the guild vault.</summary>
[DataTransferObject]
public sealed record GuildBankSlot
{
    /// <summary>The item ID of the item in this slot.</summary>
    public required int ItemId { get; init; }

    /// <summary>How many of the item are currently in this slot.</summary>
    public required int Count { get; init; }
}
