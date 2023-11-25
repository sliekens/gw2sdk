using System.Drawing;

namespace GuildWars2.Exploration.HeroChallenges;

/// <summary>Information about a hero skill point challenge</summary>
[PublicAPI]
[DataTransferObject]
public sealed record HeroChallenge
{
    /// <summary>The hero challenge ID.</summary>
    public required string Id { get; init; }

    /// <summary>The map coordinates of the hero challenge.</summary>
    public required PointF Coordinates { get; init; }
}
