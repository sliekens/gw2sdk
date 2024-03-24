namespace GuildWars2.Wvw.Matches;

/// <summary>Information about an objective in World vs. World. This type is the base type for all objectives. Cast objects
/// of this type to a more specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Objective
{
    /// <summary>The objective ID.</summary>
    public required string Id { get; init; }

    /// <summary>The color of the team that claimed the objective.</summary>
    public required TeamColor Owner { get; init; }

    /// <summary>The date and time when the objective last changed owners.</summary>
    public required DateTimeOffset LastFlipped { get; init; }

    /// <summary>The amount of points that the objective generates per tick.</summary>
    public required int PointsTick { get; init; }

    /// <summary>The amount of points that the objective generates when claimed.</summary>
    public required int PointsCapture { get; init; }
}
