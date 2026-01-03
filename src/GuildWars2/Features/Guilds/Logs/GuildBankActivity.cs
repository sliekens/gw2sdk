namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a guild vault transaction.</summary>
[DataTransferObject]
public sealed record GuildBankActivity : GuildLogEntry
{
    /// <summary>The ID of the user who performed the operation.</summary>
    public required string User { get; init; }

    /// <summary>The kind of operation performed.</summary>
    public required Extensible<GuildBankOperationKind> Operation { get; init; }

    /// <summary>The item ID of the item transaction, or 0 if this was a <see cref="Coins" /> transaction.</summary>
    public required int ItemId { get; init; }

    /// <summary>How many of the item in the transaction.</summary>
    public required int Count { get; init; }

    /// <summary>The number of coins deposited or withdrawn.</summary>
    public required Coin Coins { get; init; }
}
