namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a guild treasury deposit.</summary>
[DataTransferObject]
public sealed record TreasuryDeposit : GuildLogEntry
{
    /// <summary>The ID of the user who deposited the item.</summary>
    public required string User { get; init; }

    /// <summary>The item ID of the item deposited.</summary>
    public required int ItemId { get; init; }

    /// <summary>How many of the item were deposited.</summary>
    public required int Count { get; init; }
}
