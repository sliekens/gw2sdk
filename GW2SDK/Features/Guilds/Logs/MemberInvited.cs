namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record MemberInvited : GuildLog
{
    public required string User { get; init; }

    public required string InvitedBy { get; init; }
}