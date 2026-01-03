namespace GuildWars2.Guilds.Storage;

/// <summary>Information about an item in the guild storage.</summary>
[DataTransferObject]
public sealed record GuildStorageSlot
{
    /// <summary>The item ID of the guild consumable.</summary>
    public required int ItemId { get; init; }

    /// <summary>How many of the item currently stored.</summary>
    public required int Count { get; init; }
}
