using GuildWars2.Pvp.Games;

namespace GuildWars2.Guilds.Teams;

/// <summary>Information about a PvP game played as a team.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Game
{
    /// <summary>The game ID.</summary>
    public required string Id { get; init; }

    /// <summary>The map ID.</summary>
    public required int MapId { get; init; }

    /// <summary>The game started at this time.</summary>
    public required DateTimeOffset Started { get; init; }

    /// <summary>The game ended at this time.</summary>
    public required DateTimeOffset Ended { get; init; }

    /// <summary>The result of the game.</summary>
    public required PvpResult Result { get; init; }

    /// <summary>The color of the team.</summary>
    public required PvpTeamColor Team { get; init; }

    /// <summary>The game mode.</summary>
    public required Extensible<PvpRatingType> RatingType { get; init; }

    /// <summary>The team rating change.</summary>
    public required int RatingChange { get; init; }

    /// <summary>If present, which season this game belongs to.</summary>
    public required string? SeasonId { get; init; }

    /// <summary>The score of the game.</summary>
    public required Score Score { get; init; }
}
