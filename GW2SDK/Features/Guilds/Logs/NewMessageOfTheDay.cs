namespace GuildWars2.Guilds.Logs;

[PublicAPI]
[DataTransferObject]
public sealed record NewMessageOfTheDay : GuildLog
{
    public required string User { get; init; }

    public required string MessageOfTheDay { get; init; }
}