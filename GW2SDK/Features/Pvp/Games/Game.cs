using GuildWars2.Hero;

namespace GuildWars2.Pvp.Games;

/// <summary>Information about a PvP game played on an account.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Game
{
    /// <summary>The game ID.</summary>
    public required string Id { get; init; }

    /// <summary>The ID of the map where the game was played.</summary>
    public required int MapId { get; init; }

    /// <summary>The start time of the game.</summary>
    public required DateTimeOffset Started { get; init; }

    /// <summary>The end time of the game.</summary>
    public required DateTimeOffset Ended { get; init; }

    /// <summary>The outcome of the game for the team the account was on.</summary>
    public required PvpResult Result { get; init; }

    /// <summary>The team color the account was on.</summary>
    public required PvpTeamColor Team { get; init; }

    /// <summary>The profession of the character the account was playing.</summary>
    public required ProfessionName Profession { get; init; }

    /// <summary>The rating type of the game.</summary>
    public required PvpRatingType RatingType { get; init; }

    /// <summary>The skill rating increase or decrease of the account after the game.</summary>
    public required int RatingChange { get; init; }

    /// <summary>The ID of the season the game was played in, or <c>null</c> if the game was played in a non-seasonal context.</summary>
    public required string? SeasonId { get; init; }

    /// <summary>The final score of the game.</summary>
    public required Score Score { get; init; }
}
