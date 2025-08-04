using System.Drawing;

namespace GuildWars2.Exploration.Floors;

/// <summary>Information about a floor. Each continent has several floors, which appear on the world map as pages with
/// up/down arrows. Some floors are hidden. For example, Lion's Arch has 3 floors ingame, but 6 floors in the API.
/// Unfortunately it's not possible to tell which floors are hidden.</summary>
[DataTransferObject]
public sealed record Floor
{
    /// <summary>The floor ID.</summary>
    public required int Id { get; init; }

    /// <summary>The dimensions of the texture.</summary>
    public required Size TextureDimensions { get; init; }

    /// <summary>If present, it represents a rectangle of download-able textures. Every tile coordinate outside this rectangle
    /// is not available on the tile server.</summary>
    public required Rectangle? ClampedView { get; init; }

    /// <summary>The regions that are available on this floor. The key is the region ID, the value is the region.</summary>
    public required Dictionary<int, Regions.Region> Regions { get; init; }
}
