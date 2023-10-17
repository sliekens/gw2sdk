namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record TreasuryDeposit : GuildLog
{
    public required string User { get; init; }

    public required int ItemId { get; init; }

    public required int Count { get; init; }
}