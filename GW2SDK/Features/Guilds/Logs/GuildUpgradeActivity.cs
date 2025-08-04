namespace GuildWars2.Guilds.Logs;

/// <summary>A guild upgrade log entry.</summary>
[DataTransferObject]
public sealed record GuildUpgradeActivity : GuildLogEntry
{
    /// <summary>The user ID who performed the action.</summary>
    public required string User { get; init; }

    /// <summary>The action performed.</summary>
    public required Extensible<GuildUpgradeAction> Action { get; init; }

    /// <summary>The guild upgrade ID.</summary>
    public required int UpgradeId { get; init; }

    /// <summary>If present, the recipe ID of the Scribe recipe for the upgrade.</summary>
    public required int? RecipeId { get; init; }

    /// <summary>If present, the item ID of the item which is traded or consumed for the upgrade.</summary>
    public required int? ItemId { get; init; }

    /// <summary>If present, how many of the upgrade are created.</summary>
    public required int? Count { get; init; }
}
