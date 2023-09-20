namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[DataTransferObject]
public sealed record Worlds
{
    public required int Red { get; init; }

    public required int Blue { get; init; }

    public required int Green { get; init; }
}
