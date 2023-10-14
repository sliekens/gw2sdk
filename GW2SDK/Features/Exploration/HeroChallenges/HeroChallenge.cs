using System.Drawing;

namespace GuildWars2.Exploration.HeroChallenges;

[PublicAPI]
[DataTransferObject]
public sealed record HeroChallenge
{
    public required string Id { get; init; }

    public required PointF Coordinates { get; init; }
}
