namespace GuildWars2.Pvp.Stats;

[PublicAPI]
[DataTransferObject]
public sealed record Ladders
{
    public required Results? None { get; init; }

    public required Results? Unranked { get; init; }

    public required Results? Ranked { get; init; }

    public required Results? Ranked2v2 { get; init; }

    public required Results? Ranked3v3 { get; init; }

    public required Results? SoloArenaRated { get; init; }

    public required Results? TeamArenaRated { get; init; }
}
