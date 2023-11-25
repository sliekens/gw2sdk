using System.Drawing;

namespace GuildWars2.Exploration.Adventures;

/// <summary>Information about an adventure.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Adventure
{
    /// <summary>The adventure ID.</summary>
    public required string Id { get; init; }

    /// <summary>The adventure name.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the adventure.</summary>
    public required string Description { get; init; }

    /// <summary>The map coordinates of the adventure.</summary>
    public required PointF Coordinates { get; init; }
}
