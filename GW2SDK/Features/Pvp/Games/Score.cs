namespace GuildWars2.Pvp.Games;

[PublicAPI]
[DataTransferObject]
public sealed record Score
{
    public required int Red { get; init; }

    public required int Blue { get; init; }
}
