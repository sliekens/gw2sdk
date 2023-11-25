namespace GuildWars2.Exploration.PointsOfInterest;

/// <summary>Information about a point of interest which is locked by default, like a dungeon entrance.</summary>
[PublicAPI]
public sealed record RequiresUnlockPointOfInterest : PointOfInterest
{
    /// <summary>The icon URL of the point of interest.</summary>
    public required string IconHref { get; init; }
}
