namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record GuildUpgradeActivity : GuildLog
{
    public required string User { get; init; }

    public required GuildUpgradeAction Action { get; init; }

    public required int UpgradeId { get; init; }

    /// <summary></summary>
    /// <remarks>Can be <c>null</c>.</remarks>
    public required int? RecipeId { get; init; }

    /// <summary></summary>
    /// <remarks>Can be <c>null</c>.</remarks>
    public required int? ItemId { get; init; }

    /// <summary></summary>
    /// <remarks>Can be <c>null</c>.</remarks>
    public required int? Count { get; init; }
}
