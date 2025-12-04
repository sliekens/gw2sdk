namespace GuildWars2.Pve.MapChests;

/// <summary>Information about a map chest which can be earned once per day through meta-event completion.</summary>
[DataTransferObject]
public sealed record MapChest
{
    /// <summary>The map chest ID.</summary>
    public required string Id { get; init; }
}
