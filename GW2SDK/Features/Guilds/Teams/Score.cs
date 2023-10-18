namespace GuildWars2.Guilds.Teams;

[PublicAPI]
[DataTransferObject]
public sealed record Score
{
    public required int Red { get; init; }

    public required int Blue { get; init; }
}
