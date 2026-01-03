namespace GuildWars2.Hero.Masteries;

/// <summary>Information about a level of mastery within a mastery track, for example Gliding Basics.</summary>
[DataTransferObject]
public sealed record Mastery
{
    /// <summary>The name of the mastery.</summary>
    public required string Name { get; init; }

    /// <summary>Contains relevant information for players who have not yet unlocked the mastery.</summary>
    public required string Description { get; init; }

    /// <summary>Contains relevant information for players who have already unlocked the mastery.</summary>
    public required string Instruction { get; init; }

    /// <summary>The URL of the mastery icon that appears in the masteries panel.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the mastery icon that appears in the masteries panel.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The number of mastery points required to unlock the mastery.</summary>
    public required int PointCost { get; init; }

    /// <summary>The amount of experience required to unlock the mastery.</summary>
    public required int ExperienceCost { get; init; }
}
