namespace GuildWars2.Guilds.Storage;

[PublicAPI]
[DataTransferObject]
public sealed record GuildStorageSlot
{
    /// <summary>ID of the guild consumable.</summary>
    public required int ItemId { get; init; }

    /// <summary>Amount of the consumable in storage.</summary>
    public required int Count { get; init; }
}
