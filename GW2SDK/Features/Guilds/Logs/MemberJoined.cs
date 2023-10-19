namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record MemberJoined : GuildLog
{
    public required string User { get; init; }
}
