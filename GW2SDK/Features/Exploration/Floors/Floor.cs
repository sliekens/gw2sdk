using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;
using Region = GW2SDK.Exploration.Regions.Region;

namespace GW2SDK.Exploration.Floors;

[PublicAPI]
[DataTransferObject]
public sealed record Floor
{
    public int Id { get; init; }

    public SizeF TextureDimensions { get; init; }

    /// <summary>If present, it represents a rectangle of download-able textures. Every tile coordinate outside this rectangle
    /// is not available on the tile server.</summary>
    public MapView? ClampedView { get; init; }

    public Dictionary<int, Region> Regions { get; init; } = new(0);
}
