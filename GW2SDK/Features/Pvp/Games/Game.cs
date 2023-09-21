namespace GuildWars2.Pvp.Games;

[PublicAPI]
[DataTransferObject]
public sealed record Game
{
    public required string Id { get; init; }

    public required int MapId { get; init; }

    public required DateTimeOffset Started { get; init; }

    public required DateTimeOffset Ended { get; init; }

    public required GameResult Result { get; init; }

    public required TeamColor Team { get; init; }

    public required ProfessionName Profession { get; init; }

    public required RatingType RatingType { get; init; }

    public required int RatingChange { get; init; }

    public required string? SeasonId { get; init; }

    public required Score Score { get; init; }
}
