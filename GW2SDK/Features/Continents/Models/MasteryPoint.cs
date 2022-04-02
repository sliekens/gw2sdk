using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record MasteryPoint
    {
        public int Id { get; init; }

        public MasteryRegionName Region { get; init; }

        public IReadOnlyCollection<double> Coordinates { get; init; } = Array.Empty<double>();
    }
}
