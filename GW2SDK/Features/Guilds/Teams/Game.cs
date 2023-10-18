namespace GuildWars2.Guilds.Teams;

[PublicAPI]
[DataTransferObject]
public sealed record Game
{
    public required string Id { get; init; }

    public required int MapId { get; init; }

    public required DateTimeOffset Started { get; init; }

    public required DateTimeOffset Ended { get; init; }

    public required PvpResult Result { get; init; }

    public required PvpTeamColor Team { get; init; }

    public required PvpRatingType RatingType { get; init; }

    public required int RatingChange { get; init; }

    public required string? SeasonId { get; init; }

    public required Score Score { get; init; }
}
