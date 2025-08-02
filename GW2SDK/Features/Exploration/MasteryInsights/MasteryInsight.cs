using System.Drawing;

using GuildWars2.Hero.Masteries;

namespace GuildWars2.Exploration.MasteryInsights;

/// <summary>Information about a mastery insight location.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record MasteryInsight
{
    /// <summary>The mastery insight ID.</summary>
    public required int Id { get; init; }

    /// <summary>The associated region.</summary>
    public required Extensible<MasteryRegionName> Region { get; init; }

    /// <summary>The map coordinates of the mastery insight.</summary>
    public required PointF Coordinates { get; init; }
}
