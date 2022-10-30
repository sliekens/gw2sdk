using System;
using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Continents;

[PublicAPI]
[DataTransferObject]
public sealed record Continent
{
    /// <summary>The id of the continent.</summary>
    public int Id { get; init; }

    /// <summary>The name of the continent.</summary>
    public string Name { get; init; } = "";

    /// <summary>The width and height dimensions of the continent.</summary>
    public Size ContinentDimensions { get; init; }

    /// <summary>The minimal zoom level for use with the map tile service.</summary>
    public int MinZoom { get; init; }

    /// <summary>The maximum zoom level for use with the map tile service.</summary>
    public int MaxZoom { get; init; }

    /// <summary>A list of floors ids available for this continent.</summary>
    public IReadOnlyCollection<int> Floors { get; init; } = Array.Empty<int>();
}
