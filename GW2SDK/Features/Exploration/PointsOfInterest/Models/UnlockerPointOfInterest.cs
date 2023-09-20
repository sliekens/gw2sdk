namespace GuildWars2.Exploration.PointsOfInterest;

[PublicAPI]
public sealed record UnlockerPointOfInterest : PointOfInterest
{
    public required string Icon { get; init; }
}
