namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record GuildUpgradeActivity : GuildLog
{
    public required GuildUpgradeAction Action { get; init; }

    public required int UpgradeId { get; init; }

    public required int RecipeId { get; init; }
}