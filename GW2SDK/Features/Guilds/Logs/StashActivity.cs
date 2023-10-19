namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record StashActivity : GuildLog
{
    public required string User { get; init; }

    public required StashOperation Operation { get; init; }

    public required int ItemId { get; init; }

    public required int Count { get; init; }

    public required Coin Coins { get; init; }
}
