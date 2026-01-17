namespace GuildWars2.Hero.Masteries;

/// <summary>Information about a mastery track, for example Gliding.</summary>
[DataTransferObject]
public sealed record MasteryTrack
{
    /// <summary>The mastery track ID.</summary>
    public required int Id { get; init; }

    /// <summary>The mastery track name.</summary>
    public required string Name { get; init; }

    /// <summary>A description of what needs to be done to unlock the mastery track, for example completing a story chapter.</summary>
    public required string Requirement { get; init; }

    /// <summary>The display order of the mastery track within its region.</summary>
    /// <remarks>To sort mastery tracks, first group by <see cref="Region" />, then sort by this property.</remarks>
    public required int Order { get; init; }

    /// <summary>The URI of the mastery track background image as it appears in the masteries panel.</summary>
    public required Uri BackgroundUrl { get; init; }

    /// <summary>The region to which this mastery track belongs. The mastery track can only be progressed by earning experience
    /// in the region indicated by this property.</summary>
    public required Extensible<MasteryRegionName> Region { get; init; }

    /// <summary>The levels of mastery that can be progressed within this mastery track.</summary>
    public required IImmutableValueList<Mastery> Masteries { get; init; }
}
