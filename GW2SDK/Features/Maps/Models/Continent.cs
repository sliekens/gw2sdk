using System;
using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Maps.Models;

[PublicAPI]
[DataTransferObject]
public sealed record Continent
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public Size ContinentDimensions { get; init; }

    public int MinZoom { get; init; }

    public int MaxZoom { get; init; }

    public IReadOnlyCollection<int> Floors { get; init; } = Array.Empty<int>();
}
