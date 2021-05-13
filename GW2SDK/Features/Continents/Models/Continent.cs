﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Continent
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public int[] ContinentDimensions { get; init; } = new int[0];

        public int MinZoom { get; init; }

        public int MaxZoom { get; init; }

        public int[] Floors { get; init; } = new int[0];
    }
}
