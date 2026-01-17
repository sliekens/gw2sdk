using System.Drawing;

namespace GuildWars2.Exploration.Hearts;

/// <summary>Information about a renown heart, which is a task required for map completion.</summary>
[DataTransferObject]
public sealed record Heart
{
    /// <summary>The heart ID.</summary>
    public required int Id { get; init; }

    /// <summary>Instructions on how to complete the heart.</summary>
    public required string Objective { get; init; }

    /// <summary>The level requirement of the heart, which also determines the rewarded amount of karma, coins and experience.</summary>
    public required int Level { get; init; }

    /// <summary>The map coordinates of the heart.</summary>
    public required PointF Coordinates { get; init; }

    /// <summary>The map coordinates of the polygon corners which indicates the boundaries of the heart.</summary>
    public required IImmutableValueList<PointF> Boundaries { get; init; }

    /// <summary>The chat code of the heart's nearest waypoint. This can be used to link the waypoint in the chat, but also in
    /// guild or squad messages.</summary>
    public required string ChatLink { get; init; }
}
