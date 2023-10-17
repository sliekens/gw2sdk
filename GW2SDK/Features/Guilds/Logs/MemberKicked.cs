namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record MemberKicked : GuildLog
{
    public required string User { get; init; }

    public required string KickedBy { get; init; }
}