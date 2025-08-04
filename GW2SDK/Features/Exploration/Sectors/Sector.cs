using System.Drawing;

namespace GuildWars2.Exploration.Sectors;

/// <summary>Information about a map sector, also known as an area.</summary>
[DataTransferObject]
public sealed record Sector
{
    /// <summary>The sector ID.</summary>
    public required int Id { get; init; }

    /// <summary>The sector name.</summary>
    public required string Name { get; init; }

    /// <summary>The sector level.</summary>
    public required int Level { get; init; }

    /// <summary>The map coordinates of the sector (center position).</summary>
    public required PointF Coordinates { get; init; }

    /// <summary>The map coordinates of the polygon corners which indicates the boundaries of the sector.</summary>
    public required IReadOnlyList<PointF> Boundaries { get; init; }

    /// <summary>The chat code of the sector's nearest waypoint. This can be used to link the waypoint in the chat, but also in
    /// guild or squad messages.</summary>
    public required string ChatLink { get; init; }
}
