namespace GuildWars2.Guilds.Bank;

/// <summary>An item in the guild vault.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GuildBankSlot
{
    /// <summary>ID of the item in this slot.</summary>
    public required int ItemId { get; init; }

    /// <summary>How many of the item are currently in this slot.</summary>
    public required int Count { get; init; }
}
