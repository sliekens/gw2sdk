namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record InviteDeclined : GuildLog
{
    public required string User { get; init; }

    public required string DeclinedBy { get; init; }
}
