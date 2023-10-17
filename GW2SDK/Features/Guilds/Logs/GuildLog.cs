namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
[Inheritable]
public record GuildLog
{
    public required int Id { get; init; }

    public required DateTimeOffset Time { get; init; }
}
